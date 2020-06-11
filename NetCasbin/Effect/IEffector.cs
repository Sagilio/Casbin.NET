﻿using System;

namespace NetCasbin.Effect
{
    /// <summary>
    /// Effector is the interface for Casbin effectors.
    /// </summary>
    public interface IEffector
    {
        /// <summary>
        /// mergeEffects merges all matching results collected by the enforcer into a single decision.
        /// </summary>
        /// <param name="expr">the expression of [policy_effect].</param>
        /// <param name="effects">the effects of all matched rules.</param>
        /// <param name="results">the matcher results of all matched rules.</param>
        /// <returns>the final effect.</returns>
        bool MergeEffects(string expr, Effect[] effects, float[] results);
    }
}
