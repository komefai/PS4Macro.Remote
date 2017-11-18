// PS4Macro.Remote (File: KeyboardMap.cs)
//
// Copyright (c) 2017 Komefai
//
// Visit http://komefai.com for more information
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using PS4MacroAPI;

namespace PS4Macro.Remote
{
    public class KeyboardMap
    {
        public Dictionary<Keys, BaseAction> KeysDict { get; set; }

        public KeyboardMap()
        {
            KeysDict = new Dictionary<Keys, BaseAction>();
        }

        public void ExecuteActionsByKey(Script script, List<Keys> keys)
        {
            var state = new DualShockState();

            foreach (var key in keys)
            {
                if (key == Keys.None) continue;

                try
                {
                    BaseAction action = KeysDict[key];
                    ExecuteAction(script, action, state);
                }
                catch (KeyNotFoundException)
                {

                }
                catch (ArgumentException)
                {
                    
                }
            }
        }

        private void ExecuteAction(Script script, BaseAction action, DualShockState state = null)
        {
            if (action == null)
                return;

            // Test remap action
            {
                MappingAction cast = action as MappingAction;
                if (cast != null)
                {
                    ExecuteRemapAction(script, cast, state);
                    return;
                }
            }
            // Test macro action
            {
                MacroAction cast = action as MacroAction;
                if (cast != null)
                {
                    ExecuteMacroAction(script, cast);
                    return;
                }
            }
        }

        private void ExecuteRemapAction(Script script, MappingAction action, DualShockState state)
        {
            if (state == null)
                state = new DualShockState();

            // Try to set property using Reflection
            bool didSetProperty = false;
            try
            {
                SetValue(state, action.Property, action.Value);
                didSetProperty = true;
            }
            catch(Exception ex) { Debug.WriteLine(ex.StackTrace); }

            if (didSetProperty)
            {
                script.StopMacro();
                script.SetButtons(state);
            }
        }

        private void ExecuteMacroAction(Script script, MacroAction action)
        {
            // TODO: Load sequence from cache
            //List<DualShockState> sequence = new List<DualShockState>();

            script.StopMacro();
            script.PlayMacro(action.Path);
        }

        // https://stackoverflow.com/questions/13270183/type-conversion-issue-when-setting-property-through-reflection
        private static void SetValue(object inputObject, string propertyName, object propertyVal)
        {
            //find out the type
            Type type = inputObject.GetType();

            //get the property information based on the type
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

            //find the property type
            Type propertyType = propertyInfo.PropertyType;

            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

            //Returns an System.Object with the specified System.Type and whose value is
            //equivalent to the specified object.
            propertyVal = Convert.ChangeType(propertyVal, targetType);

            //Set the value of the property
            propertyInfo.SetValue(inputObject, propertyVal, null);

        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}
