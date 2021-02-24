﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casbin.Extensions
{
    public static class ManagementEnforcerExtension
    {
        #region "p" (Policy) Management

        #region Get Policy Items (sub, obj, act)

        /// <summary>
        /// Gets the list of subjects that show up in the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <returns>
        /// All the subjects in "p" policy rules. It actually collects the
        /// 0-index elements of "p" policy rules. So make sure your subject
        /// is the 0-index element, like (sub, obj, act). Duplicates are removed.
        /// </returns>
        public static List<string> GetAllSubjects(this IEnforcer enforcer)
        {
            return GetAllNamedSubjects(enforcer, PermConstants.Section.PolicySection);
        }

        /// <summary>
        /// GetAllNamedSubjects gets the list of subjects that show up in the currentnamed policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <returns>
        /// All the subjects in policy rules of the ptype type. It actually
        /// collects the 0-index elements of the policy rules.So make sure
        /// your subject is the 0-index element, like (sub, obj, act).
        /// Duplicates are removed.</returns>
        public static List<string> GetAllNamedSubjects(this IEnforcer enforcer, string ptype)
        {
            return enforcer.Model.GetValuesForFieldInPolicy(PermConstants.Section.PolicySection, ptype, 0);
        }

        /// <summary>
        /// Gets the list of objects that show up in the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <returns>
        /// All the objects in "p" policy rules. It actually collects the
        /// 1-index elements of "p" policy rules.So make sure your object
        /// is the 1-index element, like (sub, obj, act).
        /// Duplicates are removed.</returns>
        public static List<string> GetAllObjects(this IEnforcer enforcer)
        {
            return GetAllNamedObjects(enforcer, PermConstants.Section.PolicySection);
        }

        /// <summary>
        /// Gets the list of objects that show up in the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <returns>
        /// All the objects in policy rules of the ptype type. It actually
        /// collects the 1-index elements of the policy rules.So make sure
        /// your object is the 1-index element, like (sub, obj, act).
        /// Duplicates are removed.</returns>
        public static List<string> GetAllNamedObjects(this IEnforcer enforcer, string ptype)
        {
            return enforcer.Model.GetValuesForFieldInPolicy(PermConstants.DefaultPolicyType, ptype, 1);
        }

        /// <summary>
        /// Gets the list of actions that show up in the current policy.
        /// </summary>
        /// <returns>
        /// All the actions in "p" policy rules. It actually collects
        /// the 2-index elements of "p" policy rules.So make sure your action
        /// is the 2-index element, like (sub, obj, act).
        /// Duplicates are removed.</returns>
        public static List<string> GetAllActions(this IEnforcer enforcer)
        {
            return GetAllNamedActions(enforcer, PermConstants.Section.PolicySection);
        }

        /// <summary>
        /// Gets the list of actions that show up in the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <returns>
        /// All the actions in policy rules of the ptype type. It actually
        /// collects the 2-index elements of the policy rules.So make sure
        /// your action is the 2-index element, like (sub, obj, act).
        /// Duplicates are removed.</returns>
        public static List<string> GetAllNamedActions(this IEnforcer enforcer, string ptype)
        {
            return enforcer.Model.GetValuesForFieldInPolicy(PermConstants.Section.PolicySection, ptype, 2);
        }

        #endregion

        #region Get Policy

        /// <summary>
        /// Gets all the authorization rules in the policy.
        /// </summary>
        /// <returns> all the "p" policy rules.</returns>
        public static List<List<string>> GetPolicy(this IEnforcer enforcer)
        {
            return GetNamedPolicy(enforcer, PermConstants.Section.PolicySection);
        }

        /// <summary>
        /// Gets all the authorization rules in the named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <returns>The "p" policy rules of the specified ptype.</returns>
        public static List<List<string>> GetNamedPolicy(this IEnforcer enforcer, string ptype)
        {
            return enforcer.Model.GetPolicy(PermConstants.Section.PolicySection, ptype);
        }

        /// <summary>
        /// Gets all the authorization rules in the policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to  match this field.</param>
        /// <returns>The filtered "p" policy rules.</returns>
        public static List<List<string>> GetFilteredPolicy(this IEnforcer enforcer, int fieldIndex, params string[] fieldValues)
        {
            return GetFilteredNamedPolicy(enforcer, PermConstants.Section.PolicySection, fieldIndex, fieldValues);
        }

        /// <summary>
        /// Gets all the authorization rules in the named policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to  match this field.</param>
        /// <returns>The filtered "p" policy rules of the specified ptype.</returns>
        public static List<List<string>> GetFilteredNamedPolicy(this IEnforcer enforcer, string ptype, int fieldIndex, params string[] fieldValues)
        {
            return enforcer.Model.GetFilteredPolicy(PermConstants.Section.PolicySection, ptype, fieldIndex, fieldValues);
        }

        #endregion End of "p" (Policy) Management

        #region Has Policy

        /// <summary>
        /// Determines whether an authorization rule exists.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="paramList">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Whether the rule exists.</returns>
        public static bool HasPolicy(this IEnforcer enforcer, List<string> paramList)
        {
            return HasNamedPolicy(enforcer, PermConstants.DefaultPolicyType, paramList);
        }

        /// <summary>
        /// Determines whether an authorization rule exists.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Whether the rule exists.</returns>
        public static bool HasPolicy(this IEnforcer enforcer, params string[] parameters)
        {
            return HasPolicy(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Determines whether a named authorization rule exists.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="paramList">The "p" policy rule.</param>
        /// <returns>Whether the rule exists.</returns>
        public static bool HasNamedPolicy(this IEnforcer enforcer, string ptype, List<string> paramList)
        {
            return enforcer.Model.HasPolicy(PermConstants.Section.PolicySection, ptype, paramList);
        }

        /// <summary>
        /// Determines whether a named authorization rule exists.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Whether the rule exists.</returns>
        public static bool HasNamedPolicy(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return HasNamedPolicy(enforcer, ptype, parameters.ToList());
        }

        #endregion

        #region Add Policy

        /// <summary>
        /// Adds an authorization rule to the current policy. If the rule
        /// already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddPolicy(this IEnforcer enforcer, params string[] parameters)
        {
            return AddPolicy(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Adds an authorization rule to the current policy. If the rule
        /// already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddPolicyAsync(this IEnforcer enforcer, params string[] parameters)
        {
            return AddPolicyAsync(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Adds an authorization rule to the current policy. If the rule
        /// already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddPolicy(this IEnforcer enforcer, List<string> parameters)
        {
            return AddNamedPolicy(enforcer, PermConstants.DefaultPolicyType, parameters);
        }

        /// <summary>
        /// Adds an authorization rule to the current policy. If the rule
        /// already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddPolicyAsync(this IEnforcer enforcer, List<string> parameters)
        {
            return AddNamedPolicyAsync(enforcer, PermConstants.DefaultPolicyType, parameters);
        }

        /// <summary>
        /// Adds an authorization rule to the current named policy.If the
        /// rule already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddNamedPolicy(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return AddNamedPolicy(enforcer, ptype, parameters.ToList());
        }

        /// <summary>
        /// Adds an authorization rule to the current named policy.If the
        /// rule already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddNamedPolicyAsync(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return AddNamedPolicyAsync(enforcer, ptype, parameters.ToList());
        }

        /// <summary>
        /// Adds an authorization rule to the current named policy.If the
        /// rule already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddNamedPolicy(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return enforcer.InternalAddPolicy(PermConstants.Section.PolicySection, ptype, parameters);
        }

        /// <summary>
        /// Adds an authorization rule to the current named policy.If the
        /// rule already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddNamedPolicyAsync(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return enforcer.InternalAddPolicyAsync(PermConstants.Section.PolicySection, ptype, parameters);
        }

        /// <summary>
        /// Adds authorization rules to the current policy. If the rule
        /// already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="rules">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddPolicies(this IEnforcer enforcer, IEnumerable<List<string>> rules)
        {
            return AddNamedPolicies(enforcer, PermConstants.DefaultPolicyType, rules);
        }

        /// <summary>
        /// Adds authorization rules to the current policy. If the rule
        /// already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="rules">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddPoliciesAsync(this IEnforcer enforcer, IEnumerable<List<string>> rules)
        {
            return AddNamedPoliciesAsync(enforcer, PermConstants.DefaultPolicyType, rules);
        }

        /// <summary>
        /// Adds authorization rules to the current named policy.If the
        /// rule already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="rules">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddNamedPolicies(this IEnforcer enforcer, string ptype, IEnumerable<List<string>> rules)
        {
            return enforcer.InternalAddPolicies(PermConstants.Section.PolicySection, ptype, rules);
        }

        /// <summary>
        /// Adds authorization rules to the current named policy.If the
        /// rule already exists, the function returns false and the rule will not be added.
        /// Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="rules">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddNamedPoliciesAsync(this IEnforcer enforcer, string ptype, IEnumerable<List<string>> rules)
        {
            return enforcer.InternalAddPoliciesAsync(PermConstants.Section.PolicySection, ptype, rules);
        }

        #endregion

        #region Remove Policy

        /// <summary>
        /// Removes an authorization rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemovePolicy(this IEnforcer enforcer, List<string> parameters)
        {
            return RemoveNamedPolicy(enforcer, PermConstants.Section.PolicySection, parameters);
        }

        /// <summary>
        /// Removes an authorization rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemovePolicyAsync(this IEnforcer enforcer, List<string> parameters)
        {
            return RemoveNamedPolicyAsync(enforcer, PermConstants.DefaultPolicyType, parameters);
        }

        /// <summary>
        /// Removes an authorization rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemovePolicy(this IEnforcer enforcer, params string[] parameters)
        {
            return RemovePolicy(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Removes an authorization rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemovePolicyAsync(this IEnforcer enforcer, params string[] parameters)
        {
            return RemovePolicyAsync(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Removes an authorization rule from the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveNamedPolicy(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return RemoveNamedPolicy(enforcer, ptype, parameters.ToList());
        }

        /// <summary>
        /// Removes an authorization rule from the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveNamedPolicyAsync(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return RemoveNamedPolicyAsync(enforcer, ptype, parameters.ToList());
        }

        /// <summary>
        /// Removes an authorization rule from the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveNamedPolicy(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return enforcer.InternalRemovePolicy(PermConstants.Section.PolicySection, ptype, parameters);
        }

        /// <summary>
        /// Removes an authorization rule from the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="parameters">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveNamedPolicyAsync(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return enforcer.InternalRemovePolicyAsync(PermConstants.Section.PolicySection, ptype, parameters);
        }

        /// <summary>
        /// Removes authorization rules from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="rules">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemovePolicies(this IEnforcer enforcer, IEnumerable<List<string>> rules)
        {
            return RemoveNamedPolicies(enforcer, PermConstants.DefaultPolicyType, rules);
        }

        /// <summary>
        /// Removes authorization rules from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="rules">The "p" policy rule, ptype "p" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemovePoliciesAsync(this IEnforcer enforcer, IEnumerable<List<string>> rules)
        {
            return RemoveNamedPoliciesAsync(enforcer, PermConstants.DefaultPolicyType, rules);
        }

        /// <summary>
        /// Removes authorization rules from the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="rules">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveNamedPolicies(this IEnforcer enforcer, string ptype, IEnumerable<List<string>> rules)
        {
            return enforcer.InternalRemovePolicies(PermConstants.Section.PolicySection, ptype, rules);
        }

        /// <summary>
        /// Removes authorization rules from the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="rules">The "p" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveNamedPoliciesAsync(this IEnforcer enforcer, string ptype, IEnumerable<List<string>> rules)
        {
            return enforcer.InternalRemovePoliciesAsync(PermConstants.Section.PolicySection, ptype, rules);
        }

        /// <summary>
        /// Removes an authorization rule from the current policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to match this field.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveFilteredPolicy(this IEnforcer enforcer, int fieldIndex, params string[] fieldValues)
        {
            return RemoveFilteredNamedPolicy(enforcer, PermConstants.DefaultPolicyType, fieldIndex, fieldValues);
        }

        /// <summary>
        /// Removes an authorization rule from the current policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to match this field.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveFilteredPolicyAsync(this IEnforcer enforcer, int fieldIndex, params string[] fieldValues)
        {
            return RemoveFilteredNamedPolicyAsync(enforcer, PermConstants.DefaultPolicyType, fieldIndex, fieldValues);
        }


        /// <summary>
        /// Removes an authorization rule from the current named policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to  match this field.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveFilteredNamedPolicy(this IEnforcer enforcer, string ptype, int fieldIndex, params string[] fieldValues)
        {
            return enforcer.InternalRemoveFilteredPolicy(PermConstants.Section.PolicySection, ptype, fieldIndex, fieldValues);
        }

        /// <summary>
        /// Removes an authorization rule from the current named policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "p", "p2", "p3", ..</param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to  match this field.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveFilteredNamedPolicyAsync(this IEnforcer enforcer, string ptype, int fieldIndex, params string[] fieldValues)
        {
            return enforcer.InternalRemoveFilteredPolicyAsync(PermConstants.Section.PolicySection, ptype, fieldIndex, fieldValues);
        }

        #endregion

        #endregion // End of "p" (Policy) Management

        #region "g" (Grouping/Role Policy) Management

        #region Get Grouping/Role Policy Items (role)

        /// <summary>
        /// Gets the list of roles that show up in the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <returns>
        /// All the roles in "g" policy rules. It actually collects
        /// the 1-index elements of "g" policy rules. So make sure your
        /// role is the 1-index element, like (sub, role).
        /// Duplicates are removed.</returns>
        public static List<string> GetAllRoles(this IEnforcer enforcer)
        {
            return GetAllNamedRoles(enforcer, PermConstants.Section.RoleSection);
        }

        /// <summary>
        /// Gets the list of roles that show up in the current named policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <returns>
        /// All the subjects in policy rules of the ptype type. It actually
        /// collects the 0-index elements of the policy rules.So make
        /// Sure your subject is the 0-index element, like (sub, obj, act).
        /// Duplicates are removed.</returns>
        public static List<string> GetAllNamedRoles(this IEnforcer enforcer, string ptype)
        {
            return enforcer.Model.GetValuesForFieldInPolicy(PermConstants.Section.RoleSection, ptype, 1);
        }

        #endregion

        #region Has Grouping/Role Policy

        /// <summary>
        /// Determines whether a role inheritance rule exists.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Whether the rule exists.</returns>
        public static bool HasGroupingPolicy(this IEnforcer enforcer, List<string> parameters)
        {
            return HasNamedGroupingPolicy(enforcer, PermConstants.DefaultGroupingPolicyType, parameters);
        }

        /// <summary>
        /// Determines whether a role inheritance rule exists.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Whether the rule exists.</returns>
        public static bool HasGroupingPolicy(this IEnforcer enforcer, params string[] parameters)
        {
            return HasGroupingPolicy(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Determines whether a named role inheritance rule
        /// exists.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule.</param>
        /// <returns>Whether the rule exists.</returns>
        public static bool HasNamedGroupingPolicy(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return enforcer.Model.HasPolicy(PermConstants.Section.RoleSection, ptype, parameters);
        }

        /// <summary>
        /// Determines whether a named role inheritance rule
        /// exists.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule.</param>
        /// <returns>Whether the rule exists.</returns>
        public static bool HasNamedGroupingPolicy(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return HasNamedGroupingPolicy(enforcer, ptype, parameters.ToList());
        }

        #endregion

        #region Get Grouping/Role Policy

        /// <summary>
        /// Gets all the role inheritance rules in the policy.
        /// </summary>
        /// <returns>all the "g" policy rules.</returns>
        public static List<List<string>> GetGroupingPolicy(this IEnforcer enforcer)
        {
            return GetNamedGroupingPolicy(enforcer, PermConstants.DefaultGroupingPolicyType);
        }

        /// <summary>
        /// Gets all the role inheritance rules in the policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to match this field.</param>
        /// <returns>The filtered "g" policy rules.</returns>
        public static List<List<string>> GetFilteredGroupingPolicy(this IEnforcer enforcer, int fieldIndex, params string[] fieldValues)
        {
            return GetFilteredNamedGroupingPolicy(enforcer, PermConstants.DefaultGroupingPolicyType, fieldIndex, fieldValues);
        }

        /// <summary>
        /// Gets all the role inheritance rules in the policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <returns>The "g" policy rules of the specified ptype.</returns>
        public static List<List<string>> GetNamedGroupingPolicy(this IEnforcer enforcer, string ptype)
        {
            return enforcer.Model.GetPolicy(PermConstants.Section.RoleSection, ptype);
        }

        /// <summary>
        /// Gets all the role inheritance rules in the policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to match this field.</param>
        /// <returns>The filtered "g" policy rules of the specified ptype.</returns>
        public static List<List<string>> GetFilteredNamedGroupingPolicy(this IEnforcer enforcer, string ptype, int fieldIndex, params string[] fieldValues)
        {
            return enforcer.Model.GetFilteredPolicy(PermConstants.Section.RoleSection, ptype, fieldIndex, fieldValues);
        }

        #endregion

        #region Add Grouping/Role Policy

        /// <summary>
        /// Adds a role inheritance rule to the current policy. If the
        /// rule already exists, the function returns false and the rule will not be
        /// Added.Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddGroupingPolicy(this IEnforcer enforcer, params string[] parameters)
        {
            return AddGroupingPolicy(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Adds a role inheritance rule to the current policy. If the
        /// rule already exists, the function returns false and the rule will not be
        /// Added.Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddGroupingPolicyAsync(this IEnforcer enforcer, params string[] parameters)
        {
            return AddGroupingPolicyAsync(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Adds a role inheritance rule to the current policy. If the
        /// rule already exists, the function returns false and the rule will not be
        /// Added.Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddGroupingPolicy(this IEnforcer enforcer, List<string> parameters)
        {
            return AddNamedGroupingPolicy(enforcer, PermConstants.DefaultGroupingPolicyType, parameters);
        }

        /// <summary>
        /// Adds a role inheritance rule to the current policy. If the
        /// rule already exists, the function returns false and the rule will not be
        /// Added.Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddGroupingPolicyAsync(this IEnforcer enforcer, List<string> parameters)
        {
            return AddNamedGroupingPolicyAsync(enforcer, PermConstants.DefaultGroupingPolicyType, parameters);
        }

        /// <summary>
        /// Adds a named role inheritance rule to the current 
        /// policy. If the rule already exists, the function returns false and the rule
        /// will not be added. Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddNamedGroupingPolicy(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return enforcer.InternalAddPolicy(PermConstants.Section.RoleSection, ptype, parameters); ;
        }

        /// <summary>
        /// Adds a named role inheritance rule to the current 
        /// policy. If the rule already exists, the function returns false and the rule
        /// will not be added. Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static async Task<bool> AddNamedGroupingPolicyAsync(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return await enforcer.InternalAddPolicyAsync(PermConstants.Section.RoleSection, ptype, parameters);;
        }

        /// <summary>
        /// Adds a named role inheritance rule to the current 
        /// policy. If the rule already exists, the function returns false and the rule
        /// will not be added. Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddNamedGroupingPolicy(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return AddNamedGroupingPolicy(enforcer, ptype, parameters.ToList());
        }

        /// <summary>
        /// Adds roles inheritance rule to the current policy. If the
        /// rule already exists, the function returns false and the rule will not be
        /// Added.Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="rules">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddGroupingPolicies(this IEnforcer enforcer, IEnumerable<List<string>> rules)
        {
            return AddNamedGroupingPolicies(enforcer, PermConstants.DefaultGroupingPolicyType, rules);
        }

        /// <summary>
        /// Adds roles inheritance rule to the current policy. If the
        /// rule already exists, the function returns false and the rule will not be
        /// Added.Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="rules">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> AddGroupingPoliciesAsync(this IEnforcer enforcer, IEnumerable<List<string>> rules)
        {
            return AddNamedGroupingPoliciesAsync(enforcer, PermConstants.DefaultGroupingPolicyType, rules);
        }

        /// <summary>
        /// Adds named roles inheritance rule to the current 
        /// policy. If the rule already exists, the function returns false and the rule
        /// will not be added. Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="rules">The "g" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool AddNamedGroupingPolicies(this IEnforcer enforcer, string ptype, IEnumerable<List<string>> rules)
        {
            return enforcer.InternalAddPolicies(PermConstants.Section.RoleSection, ptype, rules);;
        }

        /// <summary>
        /// Adds named roles inheritance rule to the current 
        /// policy. If the rule already exists, the function returns false and the rule
        /// will not be added. Otherwise the function returns true by adding the new rule.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="rules">The "g" policy rule.</param>
        /// <returns>Succeeds or not.</returns>
        public static async Task<bool> AddNamedGroupingPoliciesAsync(this IEnforcer enforcer, string ptype, IEnumerable<List<string>> rules)
        {
            return await enforcer.InternalAddPoliciesAsync(PermConstants.Section.RoleSection, ptype, rules);;
        }

        #endregion

        #region Remove Grouping/Role Policy

        /// <summary>
        /// Removes a role inheritance rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveGroupingPolicy(this IEnforcer enforcer, params string[] parameters)
        {
            return RemoveGroupingPolicy(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Removes a role inheritance rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveGroupingPolicyAsync(this IEnforcer enforcer, params string[] parameters)
        {
            return RemoveGroupingPolicyAsync(enforcer, parameters.ToList());
        }

        /// <summary>
        /// Removes a role inheritance rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveGroupingPolicy(this IEnforcer enforcer, List<string> parameters)
        {
            return RemoveNamedGroupingPolicy(enforcer, PermConstants.DefaultGroupingPolicyType, parameters);
        }

        /// <summary>
        /// Removes a role inheritance rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveGroupingPolicyAsync(this IEnforcer enforcer, List<string> parameters)
        {
            return RemoveNamedGroupingPolicyAsync(enforcer, PermConstants.DefaultGroupingPolicyType, parameters);
        }

        /// <summary>
        /// Removes a role inheritance rule from the current 
        /// policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveNamedGroupingPolicy(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return RemoveNamedGroupingPolicy(enforcer, ptype, parameters.ToList());
        }

        /// <summary>
        /// Removes a role inheritance rule from the current 
        /// policy, field filters can be specified.
        /// </summary>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveNamedGroupingPolicyAsync(this IEnforcer enforcer, string ptype, params string[] parameters)
        {
            return RemoveNamedGroupingPolicyAsync(enforcer, ptype, parameters.ToList());
        }

        /// <summary>
        /// Removes a role inheritance rule from the current 
        /// policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveNamedGroupingPolicy(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return enforcer.InternalRemovePolicy(PermConstants.Section.RoleSection, ptype, parameters); ;
        }

        /// <summary>
        /// Removes a role inheritance rule from the current 
        /// policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="parameters">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static async Task<bool> RemoveNamedGroupingPolicyAsync(this IEnforcer enforcer, string ptype, List<string> parameters)
        {
            return await enforcer.InternalRemovePolicyAsync(PermConstants.Section.RoleSection, ptype, parameters); ;
        }

        /// <summary>
        /// Removes roles inheritance rule from the current policy.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="rules">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveGroupingPolicies(this IEnforcer enforcer, IEnumerable<List<string>> rules)
        {
            return RemoveNamedGroupingPolicies(enforcer, PermConstants.DefaultGroupingPolicyType, rules);

        }

        /// <summary>
        /// Removes roles inheritance rule from the current policy.
        /// </summary>
        /// <param name="rules">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveGroupingPoliciesAsync(this IEnforcer enforcer, IEnumerable<List<string>> rules)
        {
            return RemoveNamedGroupingPoliciesAsync(enforcer, PermConstants.DefaultGroupingPolicyType, rules);
        }

        /// <summary>
        /// Removes roles inheritance rule from the current 
        /// policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="rules">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveNamedGroupingPolicies(this IEnforcer enforcer, string ptype, IEnumerable<List<string>> rules)
        {
            return enforcer.InternalRemovePolicies(PermConstants.Section.RoleSection, ptype, rules); ;
        }

        /// <summary>
        /// Removes roles inheritance rule from the current 
        /// policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="rules">The "g" policy rule, ptype "g" is implicitly used.</param>
        /// <returns>Succeeds or not.</returns>
        public static async Task<bool> RemoveNamedGroupingPoliciesAsync(this IEnforcer enforcer, string ptype, IEnumerable<List<string>> rules)
        {
            return await enforcer.InternalRemovePoliciesAsync(PermConstants.Section.RoleSection, ptype, rules); ;
        }

        /// <summary>
        /// Removes a role inheritance rule from the current 
        /// policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to match this field.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveFilteredGroupingPolicy(this IEnforcer enforcer, int fieldIndex, params string[] fieldValues)
        {
            return RemoveFilteredNamedGroupingPolicy(enforcer, PermConstants.DefaultGroupingPolicyType, fieldIndex, fieldValues); ;
        }

        /// <summary>
        /// Removes a role inheritance rule from the current 
        /// policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to match this field.</param>
        /// <returns>Succeeds or not.</returns>
        public static Task<bool> RemoveFilteredGroupingPolicyAsync(this IEnforcer enforcer, int fieldIndex, params string[] fieldValues)
        {
            return RemoveFilteredNamedGroupingPolicyAsync(enforcer, PermConstants.DefaultGroupingPolicyType, fieldIndex, fieldValues); ;
        }

        /// <summary>
        /// Removes a role inheritance rule from the current named policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to match this field.</param>
        /// <returns>Succeeds or not.</returns>
        public static bool RemoveFilteredNamedGroupingPolicy(this IEnforcer enforcer, string ptype, int fieldIndex, params string[] fieldValues)
        {
            return enforcer.InternalRemoveFilteredPolicy(PermConstants.Section.RoleSection, ptype, fieldIndex, fieldValues); ;
        }

        /// <summary>
        /// Removes a role inheritance rule from the current named policy, field filters can be specified.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="ptype">The policy type, can be "g", "g2", "g3", ..</param>
        /// <param name="fieldIndex">The policy rule's start index to be matched.</param>
        /// <param name="fieldValues">The field values to be matched, value "" means not to match this field.</param>
        /// <returns>Succeeds or not.</returns>
        public static async Task<bool> RemoveFilteredNamedGroupingPolicyAsync(this IEnforcer enforcer, string ptype, int fieldIndex, params string[] fieldValues)
        {
            return await enforcer.InternalRemoveFilteredPolicyAsync(PermConstants.Section.RoleSection, ptype, fieldIndex, fieldValues); ;
        }

        #endregion

        #endregion // End of "g" (Grouping/Role Policy) Management

        /// <summary>
        /// Adds a customized function.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="name">The name of the new function.</param>
        /// <param name="function">The function.</param>
        public static void AddFunction(this IEnforcer enforcer, string name, Delegate function)
        {
            enforcer.ExpressionHandler.SetFunction(name, function);
        }

        /// <summary>
        /// Adds a customized function.
        /// </summary>
        /// <param name="enforcer"></param>
        /// <param name="name">The name of the new function.</param>
        /// <param name="function">The function.</param>
        public static void AddFunction(this IEnforcer enforcer, string name, Func<string, string, bool> function)
        {
            AddFunction(enforcer, name, (Delegate) function);
        }
    }
}
