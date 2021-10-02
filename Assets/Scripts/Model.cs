using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    class Model
    {
    }
    public enum MovementState
    {
        Idle, Run, Jump, Fall
    }

    public static class Axis
    {
        public static string Horizontal = "Horizontal";
        public static string Vertical = "Vertical";
    }
}