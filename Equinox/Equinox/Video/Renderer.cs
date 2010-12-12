using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Equinox.Objects;

namespace Equinox.Video
{
    class Renderer
    {
        protected Game game;
        private float fov;
        private float aspect;

        public Renderer(Game game)
        {
            this.game = game;

            fov = MathHelper.ToRadians(45f);
            aspect = game.GraphicsDevice.Viewport.AspectRatio;
        }

        /// <summary>
        /// Given a scene, render all the objects in the scene.
        /// </summary>
        /// <param name="scene">Scene to be rendered</param>
        /// <param name="camera">Camera object</param>
        public void Draw(Scene scene, GameObject camera)
        {
            List<GameObject> objects = scene.Visible();

            game.GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix worldMatrix = Matrix.Identity;
            Matrix cameraMatrix = camera.position.matrix();
            Matrix objectMatrix;

            foreach (GameObject obj in objects)
            {
                objectMatrix = worldMatrix * obj.position.matrix();
                RenderObject(obj, cameraMatrix, objectMatrix);
            }
        }

        /// <summary>
        /// Render an individual game object
        /// </summary>
        /// <param name="obj">Game object to be rendered</param>
        /// <param name="cameraMatrix">Camera view matrix</param>
        /// <param name="objectMatrix">Object position matrix</param>
        public void RenderObject(GameObject obj, Matrix cameraMatrix, Matrix objectMatrix)
        {
            Model model = obj.model;
            Matrix modelMatrix = Matrix.CreateScale(obj.scale) * objectMatrix;
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            // TODO: This needs to be moved to a camera class.  It only needs to be set
            //       once (generally), not every frame.
            Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(fov, aspect, 1f, 1000f);


            // TEST:  Simple lights
            Vector3 diffuseLight1Pos = new Vector3(0f, 100f, 0f);
            Vector4 diffuseLight1Color = new Vector4(1f, 1f, 1f, 1f);
            float diffuseLight1Intensity = 2f;

            Vector4 ambientLightColor = new Vector4(1f, 1f, 1f, 1f);
            float ambientLightIntensity = 0.35f;


            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    Matrix worldMatrix = transforms[mesh.ParentBone.Index] * modelMatrix;

                    // Deal with fixed-function basic effects
                    if ( effect is BasicEffect)
                    {
                        BasicEffect basicEffect = effect as BasicEffect;

                        basicEffect.World = transforms[mesh.ParentBone.Index] * modelMatrix;
                        basicEffect.View = cameraMatrix;
                        basicEffect.Projection = projectionMatrix;
                        basicEffect.EnableDefaultLighting();
                    }
                    else
                    // deal with custom shaders
                    {
                        // Set Matrices
                        effect.Parameters["WVPMatrix"].SetValue(worldMatrix * cameraMatrix * projectionMatrix);
                        effect.Parameters["World"].SetValue(worldMatrix);
                        effect.Parameters["View"].SetValue(cameraMatrix);
                        effect.Parameters["Projection"].SetValue(projectionMatrix);

                        // Set Scene Lights
                        effect.Parameters["DiffuseLight1Pos"].SetValue(diffuseLight1Pos);
                        effect.Parameters["DiffuseLight1Color"].SetValue(diffuseLight1Color);
                        effect.Parameters["DiffuseLight1Intensity"].SetValue(diffuseLight1Intensity);

                        effect.Parameters["AmbientLightColor"].SetValue(ambientLightColor);
                        effect.Parameters["AmbientLightIntensity"].SetValue(ambientLightIntensity);
                    }
                }

                mesh.Draw();
            }
        }
    }
}
