using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Equinox.Objects
{
    class Scene
    {
        List<GameObject> objects;

        public Scene()
        {
            objects = new List<GameObject>();
        }

        public void Add(GameObject obj)
        {
            objects.Add(obj);
        }

        public void Remove(GameObject obj)
        {
            objects.Remove(obj);
        }

        public List<GameObject> Visible()
        {
            return objects;
        }
    }
}
