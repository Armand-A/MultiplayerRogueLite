using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    List<InteractionTarget> targetsInRange;
    InteractionTarget mainTarget;

    private void Awake()
    {
        targetsInRange = new List<InteractionTarget>();
    }

    public void AddTarget(InteractionTarget target)
    {
        if (targetsInRange.Contains(target)) return;
        targetsInRange.Add(target);
    }

    public void RemoveTarget(InteractionTarget target)
    {
        if (!targetsInRange.Contains(target)) return;
        targetsInRange.Remove(target);
    }

    private void Update()
    {
        if (targetsInRange.Count == 0)
        {
            if (mainTarget != null)
            {
                mainTarget.SetSelected(false);
                mainTarget = null;
            }
            return;
        }

        // if 1 target, always main target
        if (targetsInRange.Count == 1)
        {
            if (mainTarget != targetsInRange[0])
            {
                if (mainTarget != null) mainTarget.SetSelected(false);
                targetsInRange[0].SetSelected(true);
                mainTarget = targetsInRange[0];
            } 
            return;
        }

        // if >= 2 targets, set mainTarget to closest target
        if (mainTarget == null) mainTarget = targetsInRange[0]; // set mainTarget to any non-null to avoid extremely rare case of having 0 targets last frame and 2 targets this frame
        InteractionTarget newTarget = mainTarget;
        float newTargetDist = Vector3.Distance(mainTarget.transform.position, transform.position);
        foreach (InteractionTarget target in targetsInRange)
        {
            float currDist = Vector3.Distance(transform.position, target.transform.position);
            if (currDist < newTargetDist)
            {
                newTarget = target;
                newTargetDist = currDist;
            }
        }
        if (newTarget != mainTarget)
        {
            mainTarget.SetSelected(false);
            newTarget.SetSelected(true);
            mainTarget = newTarget;
        }
    }

    public void OnInteractPressed()
    {
        if (mainTarget == null) return;
        mainTarget.OnInteract();
    }
}
