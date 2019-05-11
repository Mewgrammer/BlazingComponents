using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud_In_A_Box.Components.Areas.Components
{
    public class GridViewBase<T> : ComponentBase
    {
        [Parameter]
        public T[] Data { get; set; } = new T[0];

        [Parameter]
        public RenderFragment<T> ChildContent { get; set; } = null;

        [Parameter]
        public int Columns { get; set; } = 1;

        public T[,] TwoDimData { get; set; } = new T[0,0];

        protected override void OnInit()
        {
            base.OnInit();
            if(Data.Length > 0)
            {
                TwoDimData = ConvertDataToTwoDim(Data, Columns);
            }
        }

        public T[,] ConvertDataToTwoDim(T[] singleDimArray, int columns)
        {
            int rows = (int)Math.Ceiling(singleDimArray.Length / (double)columns);
            var result = new T[rows, columns];
            var index = 0;
            for(var i = 0; i < singleDimArray.Length; i++)
            {
                for (var k = 0; k < columns; k++)
                {
                    result[i, k] = singleDimArray[index++];
                }
            }
            return result;
        }

        public T[] FlattenTwoDimArray(T[,] twoDimArray)
        {
            var result = new T[twoDimArray.GetLength(0) * twoDimArray.GetLength(1)];
            var index = 0;
            for (var i = 0; i < twoDimArray.GetLength(0); i++)
            {
                for (var k = 0; k < twoDimArray.GetLength(1); k++)
                {
                    result[index++] = twoDimArray[i, k];
                }
            }
            return result;
        }


    }
}
