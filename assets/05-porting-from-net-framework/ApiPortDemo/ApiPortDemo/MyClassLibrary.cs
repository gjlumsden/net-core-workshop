using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiPortDemo
{
    public class MyClassLibrary
    {
        public void DoSomething()
        {
            UseReflection();

            CreateDataset();

            DrawImage();
        }

        private void UseReflection()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var members = assembly.GetType().GetMembers();
        }

        private void CreateDataset()
        {
            var MyDataset = new DataSet();
        }

        private void DrawImage()
        {
            var bitmap = new Bitmap(10, 10);

            for (int i = 0; i < 9; i++)
                bitmap.SetPixel(i, i, Color.Black);
        }
    }
}
