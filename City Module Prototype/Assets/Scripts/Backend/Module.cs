using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module
{
    public GameObject visualObject;

    public abstract int modifier();
    public abstract int height();
    public abstract string GetResourcePath();
    public abstract Module Copy();
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

    public override string GetResourcePath()
    {
        return "Modules/Park"; 
    }

    public override Module Copy()
    {
        return new Park();
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

    public override string GetResourcePath()
    {
        return "Modules/House";
    }

    public override Module Copy()
    {
        return new House();
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

    public override string GetResourcePath()
    {
        return "Modules/TallBuilding";
    }

    public override Module Copy()
    {
        return new TallBuilding();
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

    public override string GetResourcePath()
    {
        return "Modules/Hospital";
    }

    public override Module Copy()
    {
        return new Hospital();
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

    public override string GetResourcePath()
    {
        return "Modules/PoliceStation";
    }

    public override Module Copy()
    {
        return new PoliceStation();
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

    public override string GetResourcePath()
    {
        return "Modules/FireDepartment";
    }

    public override Module Copy()
    {
        return new FireDepartment();
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

    public override string GetResourcePath()
    {
        return "Modules/Antenna";
    }

    public override Module Copy()
    {
        return new Antenna();
    }
}

