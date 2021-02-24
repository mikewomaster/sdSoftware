namespace HslCommunication.CNC.Fanuc
{
    using System;

    public enum CNCWorkMode
    {
        AUTO = 1,
        EDIT = 3,
        HANDLE = 4,
        INCfeed = 8,
        JOG = 5,
        MDI = 0,
        REFerence = 9,
        ReMoTe = 10,
        TeachInHandle = 7,
        TeachInJOG = 6
    }
}

