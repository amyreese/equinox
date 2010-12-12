#region File Description
//-----------------------------------------------------------------------------
// NormalMappingModelProcessor.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;
using System.ComponentModel;

namespace ToonShadingEffectPipeline
{
    /// <summary>
    /// The ToonShadingModelProcessor is used to change the material/effect applied
    /// to a model. After going through this processor, the output model will be set
    /// up to be rendered with ToonShader.fx.
    /// </summary>
    [ContentProcessor(DisplayName="Toon Shading Model Processor")]
    public class ToonShadingModelProcessor : ModelProcessor
    {
        String directory;

        public override ModelContent Process(NodeContent input,
            ContentProcessorContext context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            directory = Path.GetDirectoryName(input.Identity.SourceFilename);

            return base.Process(input, context);
        }

        // acceptableVertexChannelNames are the inputs that the normal map effect
        // expects.  The NormalMappingModelProcessor overrides ProcessVertexChannel
        // to remove all vertex channels which don't have one of these four
        // names.
        static IList<string> acceptableVertexChannelNames =
            new string[]
            {
                VertexChannelNames.TextureCoordinate(0),
                VertexChannelNames.Normal(0),
            };


        /// <summary>
        /// As an optimization, ProcessVertexChannel is overridden to remove data which
        /// is not used by the vertex shader.
        /// </summary>
        /// <param name="geometry">the geometry object which contains the 
        /// vertex channel</param>
        /// <param name="vertexChannelIndex">the index of the vertex channel
        /// to operate on</param>
        /// <param name="context">the context that the processor is operating
        /// under.  in most cases, this parameter isn't necessary; but could
        /// be used to log a warning that a channel had been removed.</param>
        protected override void ProcessVertexChannel(GeometryContent geometry,
            int vertexChannelIndex, ContentProcessorContext context)
        {
            String vertexChannelName =
                geometry.Vertices.Channels[vertexChannelIndex].Name;
            
            // if this vertex channel has an acceptable names, process it as normal.
            if (acceptableVertexChannelNames.Contains(vertexChannelName))
            {
                base.ProcessVertexChannel(geometry, vertexChannelIndex, context);
            }
            // otherwise, remove it from the vertex channels; it's just extra data
            // we don't need.
            else
            {
                geometry.Vertices.Channels.Remove(vertexChannelName);
            }
        }


        protected override MaterialContent ConvertMaterial(MaterialContent material,
            ContentProcessorContext context)
        {
            EffectMaterialContent toonShadingMaterial = new EffectMaterialContent();
            
            toonShadingMaterial.Effect = new ExternalReference<EffectContent>
                (Path.Combine( Path.GetDirectoryName(directory), "Shaders\\ToonShader.fx"));

            // copy the textures in the original material to the new normal mapping
            // material. this way the diffuse texture is preserved.
            foreach (KeyValuePair<String, ExternalReference<TextureContent>> texture
                in material.Textures)
            {
                toonShadingMaterial.Textures.Add(texture.Key, texture.Value);
            }

            return base.ConvertMaterial(toonShadingMaterial, context);
        }
    }
}