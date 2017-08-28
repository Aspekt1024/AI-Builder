using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectAttribute { }
public interface IObjectComponent { }
public interface IUnitComponent : IObjectComponent { }

public interface IUnitAttribute : IObjectAttribute
{
    void FinishedAction();
}
