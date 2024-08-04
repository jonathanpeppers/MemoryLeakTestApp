# MemoryLeakTestApp

This is a small app with 2 pages, some basic UI elements,  and is made to showcase how navigating to and away from a page with multiple UI elements stacked on top of each other leads to a crash
Our findings suggest this is a memory leak issue, because the error that is thrown before the crash is: "Garbage collector could not allocate 16384 bytes of memory for major heap section". 
We also encountered the System.OutOfMemoryException. Since is not a wise idea to manipulate the garbage collector ourselves, we think this a MAUI framework problem 

### Repro:

Navigate back and forth between the two pages of the app

### Android

Harder to reproduces, but eventually, the app will freeze or crash

### iOS

After about 6-10 navigations events, the app will crash
