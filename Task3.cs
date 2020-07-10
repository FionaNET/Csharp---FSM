using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Runtime.CompilerServices;

namespace Task_3
{
    public delegate void nextAction();
    class Program
    {
        //states for finite state machine 1
        static int S0 = 0;
        static int S1 = 1;
        static int S2 = 2;
        //states for finite state machine 2
        static int SA = 0;
        static int SB = 1;
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
            Console.WriteLine("Action X occurs in FSM1");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action X occurs in FSM1 \n";
        }
        public static void Action_Y()
        {
            Console.WriteLine("Action Y occurs in FSM1");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action Y occurs in FSM1 \n";
        }

        public static void Action_Z()
        {
            Console.WriteLine("Action Z occurs in FSM1");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action Z occurs in FSM1 \n";
        }

        public static void Do_nothing()
        {
            Console.WriteLine("No Action Occurs");
            TempData = $"{TempData} {DateTime.Now.ToString()} No Action Occurs \n";
        }

        public static void Action_W()
        {
            Console.WriteLine("Action W occurs in FSM1");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action W occurs in FSM1 \n";
        }

        public static void Action_J()
        {
            Console.WriteLine("Action J occurs in FSM2");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action J occurs in FSM2 \n";
        }

        public static void Action_K()
        {
            Console.WriteLine("Action K occurs in FSM2");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action K occurs in FSM2 \n";
        }

        public static void Action_L()
        {
            Console.WriteLine("Action L occurs in FSM2");
            TempData = $"{TempData} {DateTime.Now.ToString()} Action L occurs in FSM2 \n";
        }

        static void Main(string[] args)
        {
            //Finite State Table for the first Finite State Machine
            FiniteStateTable FSM1 = new FiniteStateTable(3, 3);
            //row 0 column 0
            FSM1.SetActions(0, 0, (nextAction)Action_X + (nextAction)Action_Y);
            FSM1.SetNextState(0, 0, S1);
            //row 0 column 1
            FSM1.SetActions(0, 1, Action_W);
            FSM1.SetNextState(0, 1, S0);
            //row 0 column 2
            FSM1.SetActions(0, 2, Action_W);
            FSM1.SetNextState(0, 2, S0);
            //row 1 column 0
            FSM1.SetActions(1, 0, Do_nothing);
            FSM1.SetNextState(1, 0, S0);
            //row 1 column 1
            FSM1.SetActions(1, 1, (nextAction)Action_X + (nextAction)Action_Z);
            FSM1.SetNextState(1, 1, S2);
            //row 1 column 2
            FSM1.SetActions(1, 2, Do_nothing);
            FSM1.SetNextState(1, 2, S2);
            //row 2 column 0
            FSM1.SetActions(2, 0, Do_nothing);
            FSM1.SetNextState(2, 0, S0);
            //row 2 column 1
            FSM1.SetActions(2, 1, Do_nothing);
            FSM1.SetNextState(2, 1, S1);
            //row 2 column 2
            FSM1.SetActions(2, 2, (nextAction)Action_X + (nextAction)Action_Y);
            FSM1.SetNextState(2, 2, S1);

            //Finite State Table for the second Finite State Machine
            FiniteStateTable FSM2 = new FiniteStateTable(3, 2);
            //row 0 column 0
            FSM2.SetActions(0, 0, Do_nothing);
            FSM2.SetNextState(0, 0, SB);
            //row 0 column 1
            FSM2.SetActions(0, 1, (nextAction)Action_J + (nextAction)Action_K + (nextAction)Action_L);
            FSM2.SetNextState(0, 1, SA);
            //row 1 column 0
            FSM2.SetActions(1, 0, Do_nothing);
            FSM2.SetNextState(1, 0, SA);
            //row 1 column 1
            FSM2.SetActions(1, 1, (nextAction)Action_J + (nextAction)Action_K + (nextAction)Action_L);
            FSM2.SetNextState(1, 1, SA);
            //row 2 column 0
            FSM2.SetActions(2, 0, Do_nothing);
            FSM2.SetNextState(2, 0, SA);
            //row 2 column 1
            FSM2.SetActions(2, 1, (nextAction)Action_J + (nextAction)Action_K + (nextAction)Action_L);
            FSM2.SetNextState(2, 1, SA);

            //switch case depending on key press? so q can be used to break
            char key_in;
            int CurEvent;
            int CurState1 = S0; //track current state of the first FSD
            int CurState2 = SB; //track current state of the second FSD
            char[] CompChar = { 'a', 'b', 'c', 'q' };

            //continously check for key input
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
                else
                { //invalid key input pressed
                    Console.WriteLine("  This is an invalid input");
                    TempData = $"{TempData} {DateTime.Now.ToString()} {key_in}  This is an invalid input \n"; //log the invalid key
                }


                //if a valid key is pressed, do the action, change the state and log the information
                if (CurEvent >= 0)
                {
                    Console.WriteLine(Environment.NewLine); //writes action on to a new line
                    FSM1.FST[CurEvent, CurState1].cellAction(); //perform action of the current event and state for FSM1
                    
                    //changing from SB to SA FSM2
                    if ((CurState1 == S1) && (CurState2 == SB)) //If the state of FSM2 is SB and "a" "b" or "c" is pressed, then perform J,K,L and go to state SA
                    {
                        Console.WriteLine("FSM2 is now in State SA"); //printing the change of state to console
                        //create thread class for each action for FSM2
                        System.Threading.Thread J = new System.Threading.Thread(new System.Threading.ThreadStart(Action_J));
                        System.Threading.Thread K = new System.Threading.Thread(new System.Threading.ThreadStart(Action_K));
                        System.Threading.Thread L = new System.Threading.Thread(new System.Threading.ThreadStart(Action_L));
                        //perform Actions J, K, L
                        J.Start();
                        K.Start();
                        L.Start();

                        //set the current state to SA
                        CurState2 = SA;
                        TempData = $"{TempData} {DateTime.Now.ToString()} FSM2 is now in State SA \n"; //logging the current state

                    } else if ((CurState2 == SA) && (CurEvent == E0)){ //if FSM2 is in state SA and the key pressed is 'a'

                        FSM2.FST[CurEvent, CurState2].cellAction(); //perform the action from SA to SB
                        CurState2 = SB; //change from state SA to SB
                        Console.WriteLine("FSM2 is now in State SB"); //printing the change of state to console
                        TempData = $"{TempData} {DateTime.Now.ToString()} FSM2 is now in State SB \n"; //logging the current state

                    }
                    else
                    { //SB + S2, SB + S3, SA + E1, SA + E2
                        Do_nothing();
                    }

                    //changing state of FSM1
                    if (CurState1 != FSM1.GetNextState(CurEvent, CurState1)) //if the current state doesn't match the next state
                    {
                        CurState1 = FSM1.GetNextState(CurEvent, CurState1); //change the state to the new state  
                        Console.WriteLine("FSM1 is now in State S" + CurState1); //printing the change of state to console
                        TempData = $"{TempData} {DateTime.Now.ToString()} FSM1 is now in State S{CurState1} \n"; //logging the current state
                    }
                }
            }
        }
    }
}
