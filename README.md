# MipsCycler

A program that can process mips commands and print out how many cycles it took to complete the commands.
The count proccess is just based on the used registers and the type of the instruction, the code is not really executed. 
Therefore the input files must be setup manually to fit be counted correct. 

The counting process can be adjusted by the command line settings, you can set:
- Forwarding (true/false) 
- Number of HazardBubbels, JumpBubbels and BranchBubbels 
- perCycleOutput(true/false) (detailed output)

As a extra the program includes a (limited) execution mode that really executes the instructions. 
I did only implement the commands that were necessary for my task at the college. (I know, I'm lazy ^^)
If the program is executed correclty it prints out a 'executing_order' file that can be used for the regular counting process.


Made for RA, 2. Semester, HTWG Konstanz
