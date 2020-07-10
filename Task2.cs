using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.CompilerServices;

namespace Task_2
{
    public delegate void nextAction();
    class Program
    {
        //states
        static int S0 = 0;
        static int S1 = 1;
        static int S2 = 2;
        //events
        static int E0 = 0;
        static int E1 = 1;
        static int E2 = 2;

        static string TempData = ""; //stores the data: timestamping user input (events) and actions

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
                FST = new cell_FST[0, 0];
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

        public static void Action_X()
        {
            Console.WriteLine("\nAction X");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action X \n";
        }
        public static void Action_Y()
        {
            Console.WriteLine("Action Y");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action Y \n";
        }

        public static void Action_Z()
        {
            Console.WriteLine("Action Z");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action Z \n";
        }

        public static void Do_nothing()
        {
            Console.WriteLine("\nNo Action Occurs");
            TempData = $"{TempData} {DateTime.Now.ToString()} No Action Occurs \n";
        }

        public static void Action_W()
        {
            Console.WriteLine("\nAction W");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action W \n";
        }

        static void Main(string[] args)
        {

            var FSM = new FiniteStateTable(3, 3);
            //row 0 column 0
            FSM.SetActions(0, 0, (nextAction)Action_X + (nextAction)Action_Y);
            FSM.SetNextState(0, 0, S1);
            //row 0 column 1
            FSM.SetActions(0, 1, Action_W);
            FSM.SetNextState(0, 1, S0);
            //row 0 column 2
            FSM.SetActions(0, 2, Action_W);
            FSM.SetNextState(0, 2, S0);
            //row 1 column 0
            FSM.SetActions(1, 0, Do_nothing);
            FSM.SetNextState(1, 0, S0);
            //row 1 column 1
            FSM.SetActions(1, 1, (nextAction)Action_X + (nextAction)Action_Z);
            FSM.SetNextState(1, 1, S2);
            //row 1 column 2
            FSM.SetActions(1, 2, Do_nothing);
            FSM.SetNextState(1, 2, S2);
            //row 2 column 0
            FSM.SetActions(2, 0, Do_nothing);
            FSM.SetNextState(2, 0, S0);
            //row 2 column 1
            FSM.SetActions(2, 1, Do_nothing);
            FSM.SetNextState(2, 1, S1);
            //row 2 column 2
            FSM.SetActions(2, 2, (nextAction)Action_X + (nextAction)Action_Y);
            FSM.SetNextState(2, 2, S1);

             
            char key_in;
            int CurEvent;
            int CurState = S0; //default state is S0
            char[] CompChar = { 'a', 'b', 'c', 'q' };

            while (true)
            {
                key_in = Console.ReadKey().KeyChar; //reads user input
                CurEvent = -1; 

                if (key_in == CompChar[0]) { CurEvent = E0; TempData = $"{TempData} {DateTime.Now.ToString()} {key_in} \n"; }      //check key input is a
                else if (key_in == CompChar[1]) { CurEvent = E1; TempData = $"{TempData} {DateTime.Now.ToString()} {key_in} \n"; } //check key input is b
                else if (key_in == CompChar[2]) { CurEvent = E2; TempData = $"{TempData} {DateTime.Now.ToString()} {key_in} \n"; } //check key input is c
                else if (key_in == CompChar[3]) //check key input is q
                {
                    bool directoryexists = false;
                    bool filenamevalid = false;
                    string Fname = "";
                    string filename = "";
                    string directoryname = "";

                    do
                    {
                        //ask for filename and filepath
                        Console.WriteLine("\nPlease enter a fully qualified filename and filepath (include extension '.txt'): ");
                        //store user input in string Fname
                        Fname = Console.ReadLine();

                        //check the filepath is valid
                        filename = Path.GetFileName(Fname); //extract the filename from the input given
                        directoryname = Path.GetDirectoryName(Fname); //extract the file location from the input given

                        //check the directory exists
                        directoryexists = (Directory.Exists(directoryname) && !string.IsNullOrEmpty(directoryname)); //if false the filepath is fake
                        filenamevalid = ((!string.IsNullOrEmpty(filename)) && (filename.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)); //check if the filename is valid by checking invalid characters and empty

                    } while (!(directoryexists && filenamevalid)); //if either does not exist it will continue to ask for valid filename and filepath

                    System.IO.File.WriteAllText(@Fname, TempData); //writes the data to the specified location
                    break; //quit application

                }
                else { //invalid key input pressed
                    Console.WriteLine("  This is an invalid input");
                    TempData = $"{TempData} {DateTime.Now.ToString()} {key_in}  This is an invalid input \n"; //log the invalid key
                }


                //if a valid key is pressed
                if (CurEvent >= 0)
                {
                    FSM.FST[CurEvent, CurState].cellAction(); //perform action of the current event and state
                    if (CurState != FSM.GetNextState(CurEvent, CurState)) //if the current state doesn't match the next state
                    {
                        CurState = FSM.GetNextState(CurEvent, CurState); //change the state to the new state  
                        Console.WriteLine("Now in State S" + CurState); //printing the change of state to console
                        TempData = $"{TempData} {DateTime.Now.ToString()} Now in State S{CurState} \n"; //logging the current state
                    }
                }
            }   
        }
    }
}
