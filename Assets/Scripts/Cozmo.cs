using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Linq;
using System.Threading;


public class Cozmo : MonoBehaviour {

    int numIndents = 0;

    public Button runButton; //Run Button Object

    public TabGroup worktabs; //Game object for work Tabs

    public Panel panel; //The main pannel that runs the program

    public VariableList variableList; //(un implemented list of variables

    ArrayList commands; //list of commands for cozmo

    

   


    // Use this for initialization
    void Start()
    {

        

        commands = new ArrayList(); //creates command list

        runButton.GetComponent<Button>(); //lets us tell which button is the run button

        worktabs.GetComponent<TabGroup>(); // lets us tell which tabgroub is the work tabs

        panel.GetComponent<Panel>(); // lets us choose the main pannel

      

        runButton.onClick.AddListener(run); // when runButton is clicked the run() method is activated



    }

    // Update is called once per frame
    void Update()
    {



              

    }

    // adds speak command to Command List
    void addSpeak(String inputSpeak)
    {

        commands.Add(new Command("speak",inputSpeak));


    }//addSpeak


    // adds MoveForward to Command List
    void addMoveForward(float dist, float sped)
    {


        commands.Add(new Command("forward", dist, sped));


    }//addMoveForward

    //adds Turn to command list
    void addTurn(float degrees)
    {

        commands.Add(new Command("turn", degrees));

    }

    // adds lift up to command list
    void addLiftUp()
    {

        commands.Add(new Command("liftUp"));

    }

    // adds lift down to command list
    void addLiftDown()
    {

        commands.Add(new Command("liftDown"));

    }


    // The run method
    void run()
    {



        foreach (Transform child in panel.transform) // for each block on the main panel
        {
            print("Block Name: " + child); // prints the block name

            switch (child.name) // switch is used instead of a bunch of if-else statments
            {

                case "PlaceHolder": // placeHolder is an invisible block added to make the blocks lineup work
                    break;

                case "Speak":

                    addSpeak(child.GetComponent<Speak>().getText());

                    print("Speak: " + child.GetComponent<Speak>().getText());

                    break;
                case "MoveForward": //TO DO: For abstraction, get numbers from MoveForward object

                    float dist = 0;
                    float sped = 0;
                    float.TryParse(child.Find("DistanceInput").GetComponent<InputField>().text, out dist);
                    float.TryParse(child.Find("SpeedInput").GetComponent<InputField>().text, out sped);

                    addMoveForward(child.GetComponent<MoveForward>().getDistance(), child.GetComponent<MoveForward>().getSpeed());

                    print("MoveForward: " + child.GetComponent<MoveForward>().getDistance() +":"+ child.GetComponent<MoveForward>().getSpeed());

                    break;

                case "liftUp":

                    addLiftUp();

                    print("LiftUp");

                    break;

                case "liftDown":

                    addLiftDown();

                    print("LiftDown");

                    break;

                case "TurnInPlace":// TO DO: For abstraction, get degrees from Turn in place object

                    float degrees = 0;

                    float.TryParse(child.Find("DegreesInput").GetComponent<InputField>().text, out degrees);



                    addTurn(degrees);

                    print("turn in place:" + degrees);

                    break;

                default:
                    break;

            }

        }//foreach


       Command[] runCommands = new Command[commands.Count]; // creates array the size of the commands list

        runCommands = commands.Cast<Command>().ToArray(); //adds all of the commands from the list into an array

        // Starts the command promp
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = false;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        // stream writer writes to the command prompt
        System.IO.StreamWriter myStream = new System.IO.StreamWriter(cmd.StandardInput.BaseStream, Encoding.ASCII);


        myStream.WriteLine("py"); // py starts python3 

        // imports for cozmo
        myStream.WriteLine("import time");
        myStream.WriteLine("import cozmo");
        myStream.WriteLine("import asyncio");
        myStream.WriteLine("from cozmo.util import degrees, distance_mm, speed_mmps");
        
        //starts cozmo function
        myStream.WriteLine("def cozmo_program(robot: cozmo.robot.Robot):");



        String rnCmd = "";

        for (int i = 0; i < runCommands.Length; i++) //loops through all commands
        {
            rnCmd = runCommands[i].getType();// gets command type i.e "speak", "moveforward", ect

            switch (rnCmd) // for each block, the command object gets output which is the python code to run the command
            {// numIndents is the number of tabs the python uses instad of {}

                case "speak":

                    myStream.WriteLine(runCommands[i].getOutput(numIndents));
                    UnityEngine.Debug.Log(runCommands[i].getOutput(numIndents)); // prints out command to debug log

                    break;

                case "forward":

                    myStream.WriteLine(runCommands[i].getOutput(numIndents));
                    UnityEngine.Debug.Log(runCommands[i].getOutput(numIndents));

                    break;

                case "turn":

                   myStream.WriteLine(runCommands[i].getOutput(numIndents));
                   UnityEngine.Debug.Log(runCommands[i].getOutput(numIndents));

                    break;


                case "setLiftHeight":

                    myStream.WriteLine(runCommands[i].getOutput(numIndents));
                    UnityEngine.Debug.Log(runCommands[i].getOutput(numIndents));

                    break;

                default:
                    break;

            }//switch

            myStream.WriteLine(""); // blank string to get out of the function (one less indent)
            myStream.WriteLine("cozmo.run_program(cozmo_program)"); // runs cozmo function
            myStream.Close(); // closes streamn writer

            //closes command prompt
            cmd.StandardInput.Flush();
            cmd.WaitForExit();




            // obsolete code replaced by switch statment
            /*if (runCommands[i].getType() == "speak")
            {

                myStream.WriteLine(runCommands[i].getOutput(numIndents));
                UnityEngine.Debug.Log(runCommands[i].getOutput(numIndents));

            }//if

            else if (runCommands[i].getType() == "forward")
            {

                myStream.WriteLine(runCommands[i].getOutput(numIndents));
                UnityEngine.Debug.Log(runCommands[i].getOutput(numIndents));

            }//else if
            else if (runCommands[i].getType() == "for")
            {

                myStream.WriteLine(runCommands[i].getOutput(numIndents));
                UnityEngine.Debug.Log(runCommands[i].getOutput(numIndents));
                numIndents++;

            }//else if
            else if (runCommands[i].getType() == "forEnd")
            {

                myStream.WriteLine(runCommands[i].getOutput(numIndents));
                UnityEngine.Debug.Log(runCommands[i].getOutput(numIndents));
                numIndents--;


            }//else if*/
        }//for





        //commands.Clear();


    }//run
}


