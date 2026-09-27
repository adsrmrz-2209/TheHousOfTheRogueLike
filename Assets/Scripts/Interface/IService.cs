using System.Collections.Generic;
using UnityEngine;

public interface IService
{
    void Init(List<IService> services);
}
