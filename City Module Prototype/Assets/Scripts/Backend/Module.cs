using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module
{
    public abstract int modifier();
    public abstract int height(); 

}

public class Park : Module
{
    public override int modifier()
    {
        return 0;
    }

    public override int height()
    {
        return 0; 
    }
}

public class House : Module
{
    public override int modifier()
    {
        return -1;
    }

    public override int height()
    {
        return 1;
    }
}

public class TallBuilding : Module
{
    public override int modifier()
    {
        return -2;
    }

    public override int height()
    {
        return 3;
    }
}

public class Hospital : Module
{
    public override int modifier()
    {
        return -2;
    }

    public override int height()
    {
        return 2;
    }
}

public class PoliceStation: Module
{
    public override int modifier()
    {
        return -1;
    }

    public override int height()
    {
        return 2;
    }
}

public class FireDepartment : Module
{
    public override int modifier()
    {
        return -1;
    }

    public override int height()
    {
        return 2;
    }
}

public class Antenna : Module
{
    public override int modifier()
    {
        return 0;
    }

    public override int height()
    {
        return 0;
    }
}

