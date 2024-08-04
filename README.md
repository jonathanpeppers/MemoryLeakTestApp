This is a small app with 2 pages, some basic UI elementes,  and is made to showcase how navigating to and away from a page with multiple UI elements stacked on top of each other leads to a crash. 
Our findings suggest this is a memory leak issue, because the error that is throw after ther crash is: "Garbage collector could not allocate 16384 bytes of memory for major heap section". 
We also encountered the System.OutOfMemoryException. Since is not a wise idea to manipulate the garbage collector, we think this a MAUI framework problem and we hope that it could be fixed in a timely manner.
