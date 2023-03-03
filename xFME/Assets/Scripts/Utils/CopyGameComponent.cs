/*
                             MIT License

                    Copyright (c) 2023 Krispy

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/
using System.Reflection;
using UnityEngine;

namespace Utils
{
    public static class CopyGameComponent
    {
        public static void CopyComponent( Component sourceComponent, GameObject targetObject )
        {
            /* Add a component to our targetObject, sourceComponent is our component to be copied  */
            Component componentToCopy = targetObject.AddComponent( sourceComponent.GetType() );

            /* Get our fields, needed to populated our newly copied Component! */
            var fields = componentToCopy.GetType().GetFields(
                    BindingFlags.Public     |
                    BindingFlags.NonPublic  |
                    BindingFlags.Instance
                );

            /* Loop trought our fields and populate our new component with's source values */
            foreach ( var field in fields ) 
            {
                field.SetValue( componentToCopy, field.GetValue( componentToCopy ) );
            }
        }
    }
}

