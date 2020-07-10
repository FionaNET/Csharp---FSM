using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.CompilerServices;

namespace Task_1
{
    public delegate void nextAction();
    class Program
    {
        class FiniteStateTable
        {
            public struct cell_FST
            {
                //holds the next state 
                public int nextState;
                //holds the action associated with the transition
                public nextAction cellAction;
            }

            public cell_FST[,] FST; //a 2D array named FST that holds cell_FST

            //FiniteStateTable constructor when the size of the 2D array is specified
            public FiniteStateTable(int eventSize, int stateSize)
            {
                FST = new cell_FST[eventSize, stateSize];
            }
            
            //FiniteStateTable constructor if the size is not specified
            public FiniteStateTable()
            {
                FST = new cell_FST[0,0];
            }

            //able to set nextState given the array index and desired next state
            public void SetNextState(int eventPos, int statePos, int next_State)
            {
                FST[eventPos, statePos].nextState = next_State;
            }

            public int GetNextState(int eventPos, int statePos)
            {
                return FST[eventPos, statePos].nextState;
            }

            //able to set cellAction given the array index and desired action
            public void SetActions(int eventPos, int statePos, nextAction next_Action)
            {
                FST[eventPos, statePos].cellAction = next_Action;
            }

            public nextAction GetActions(int eventPos, int statePos)
            {
                return FST[eventPos, statePos].cellAction;
            }

        }

        static void Main(string[] args)
        {
        }

    }
}
