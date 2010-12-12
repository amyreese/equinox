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
        private float fov;
        private float aspect;

        public Renderer()
        {
            fov = MathHelper.ToRadians(45f);
        }

        /// <summary>
        /// Given a scene, render all the objects in the scene.
        /// </summary>
        /// <param name="scene">Scene to be rendered</param>
        /// <param name="camera">Camera object</param>
        public void Draw(Scene scene, GameObject camera)
        {
            aspect = Engine.graphics.Viewport.AspectRatio;

            List<GameObject> objects = scene.Visible();

            Engine.game.GraphicsDevice.Clear(Color.Black);

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

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = transforms[mesh.ParentBone.Index] * modelMatrix;
                    effect.View = cameraMatrix;
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(fov, aspect, 1f, 1000f);
                    effect.EnableDefaultLighting();

                    mesh.Draw();
                }
            }
        }
    }
}
