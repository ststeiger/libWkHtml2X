
#if NET_2_0  

namespace System.Reflection
{


    public static class IntrospectionExtensions
    {
        public static TypeInfo GetTypeInfo(Type type)
        {
            return new TypeInfo(type);
        }
    }

    public class TypeInfo
    {
        private Type t;

        public string Name
        {
            get
            {
                return this.t.Name;
            }
        }


        // public bool IsConstructedGenericType => t.IsConstructedGenericType;

        public RuntimeTypeHandle TypeHandle
        {
            get
            {
                return t.TypeHandle;
            }
        }

        public Type DeclaringType
        {
            get
            {
                return this.t.DeclaringType;
            }
        }

        public string AssemblyQualifiedName
        {
            get
            {
                return this.t.AssemblyQualifiedName;
            }
        }

        public bool IsArray
        {
            get
            {
                return this.t.IsArray;
            }
        }

        public bool IsAnsiClass
        {
            get
            {
                return this.t.IsAnsiClass;
            }
        }

        public bool IsAbstract
        {
            get
            {
                return this.t.IsAbstract;
            }
        }

        public bool HasElementType
        {
            get
            {
                return this.t.HasElementType;
            }
        }

        public Guid GUID
        {
            get
            {
                return this.t.GUID;
            }
        }

        public int GenericParameterPosition
        {
            get
            {
                return this.t.GenericParameterPosition;
            }
        }

        public GenericParameterAttributes GenericParameterAttributes
        {
            get
            {
                return this.t.GenericParameterAttributes;
            }
        }

        public string FullName
        {
            get
            {
                return this.t.FullName;
            }
        }

        public MethodBase DeclaringMethod
        {
            get
            {
                return this.t.DeclaringMethod;
            }
        }

        public bool ContainsGenericParameters
        {
            get
            {
                return this.t.ContainsGenericParameters;
            }
        }

        public Type BaseType
        {
            get
            {
                return this.t.BaseType;
            }
        }

        public Assembly Assembly
        {
            get
            {
                return this.t.Assembly;
            }
        }

        public bool IsAutoClass
        {
            get
            {
                return this.t.IsAutoClass;
            }
        }

        // public Type[] GenericTypeArguments => t.GenericTypeArguments;


        public MemberTypes MemberType
        {
            get
            {
                return this.t.MemberType;
            }
        }

        public Type UnderlyingSystemType
        {
            get
            {
                return this.t.UnderlyingSystemType;
            }
        }

        public ConstructorInfo TypeInitializer
        {
            get
            {
                return this.t.TypeInitializer;
            }
        }

        public System.Runtime.InteropServices.StructLayoutAttribute StructLayoutAttribute
        {
            get
            {
                return this.t.StructLayoutAttribute;
            }
        }

        public System.Collections.Generic.IEnumerable<Type> ImplementedInterfaces
        {
            get
            {
                return this.t.GetInterfaces();
            }
        }

        public Type[] GenericTypeParameters
        {
            get
            {
                return this.t.IsGenericTypeDefinition ? this.t.GetGenericArguments() : Type.EmptyTypes;
            }
        }

        public System.Collections.Generic.IEnumerable<PropertyInfo> DeclaredProperties
        {
            get
            {
                return this.t.GetProperties(BindingFlags.DeclaredOnly);
            }
        }

        public System.Collections.Generic.IEnumerable<TypeInfo> DeclaredNestedTypes
        {
            get
            {
                System.Collections.Generic.List<TypeInfo> ls =
                    new System.Collections.Generic.List<TypeInfo>();

                Type[] nestedTypes = this.t.GetNestedTypes(BindingFlags.DeclaredOnly);
                for (int i = 0; i < nestedTypes.Length; i++)
                {
                    Type t = nestedTypes[i];
                    ls.Add(IntrospectionExtensions.GetTypeInfo(t));
                }
                return ls;
            }
        }

        public System.Collections.Generic.IEnumerable<MethodInfo> DeclaredMethods
        {
            get
            {
                return this.t.GetMethods(BindingFlags.DeclaredOnly);
            }
        }

        public System.Collections.Generic.IEnumerable<MemberInfo> DeclaredMembers
        {
            get
            {
                return this.t.GetMembers(BindingFlags.DeclaredOnly);
            }
        }

        public System.Collections.Generic.IEnumerable<FieldInfo> DeclaredFields
        {
            get
            {
                return this.t.GetFields(BindingFlags.DeclaredOnly);
            }
        }

        public System.Collections.Generic.IEnumerable<EventInfo> DeclaredEvents
        {
            get
            {
                return this.t.GetEvents(BindingFlags.DeclaredOnly);
            }
        }

        public TypeAttributes Attributes
        {
            get
            {
                return this.t.Attributes;
            }
        }

        public bool IsAutoLayout
        {
            get
            {
                return this.t.IsAutoLayout;
            }
        }

        public bool IsByRef
        {
            get
            {
                return this.t.IsByRef;
            }
        }

        public bool IsClass
        {
            get
            {
                return this.t.IsClass;
            }
        }

        public bool IsValueType
        {
            get
            {
                return this.t.IsValueType;
            }
        }

        public bool IsUnicodeClass
        {
            get
            {
                return this.t.IsUnicodeClass;
            }
        }

        public bool IsSpecialName
        {
            get
            {
                return this.t.IsSpecialName;
            }
        }

        public bool IsSerializable
        {
            get
            {
                return this.t.IsSerializable;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.t.IsVisible;
            }
        }

        public bool IsSealed
        {
            get
            {
                return this.t.IsSealed;
            }
        }

        public bool IsPublic
        {
            get
            {
                return this.t.IsPublic;
            }
        }

        public bool IsPrimitive
        {
            get
            {
                return this.t.IsPrimitive;
            }
        }

        public bool IsPointer
        {
            get
            {
                return this.t.IsPointer;
            }
        }

        public bool IsNotPublic
        {
            get
            {
                return this.t.IsNotPublic;
            }
        }

        public bool IsNestedPublic
        {
            get
            {
                return this.t.IsNestedPublic;
            }
        }

        public bool IsNestedPrivate
        {
            get
            {
                return this.t.IsNestedPrivate;
            }
        }

        public bool IsNestedFamORAssem
        {
            get
            {
                return this.t.IsNestedFamORAssem;
            }
        }

        public bool IsNestedFamily
        {
            get
            {
                return this.t.IsNestedFamily;
            }
        }

        public bool IsNestedFamANDAssem
        {
            get
            {
                return this.t.IsNestedFamANDAssem;
            }
        }

        public bool IsNestedAssembly
        {
            get
            {
                return this.t.IsNestedAssembly;
            }
        }

        public bool IsNested
        {
            get
            {
                return this.t.IsNested;
            }
        }

        public bool IsMarshalByRef
        {
            get
            {
                return this.t.IsMarshalByRef;
            }
        }

        public bool IsLayoutSequential
        {
            get
            {
                return this.t.IsLayoutSequential;
            }
        }

        public bool IsInterface
        {
            get
            {
                return this.t.IsInterface;
            }
        }

        public bool IsImport
        {
            get
            {
                return this.t.IsImport;
            }
        }

        public bool IsGenericTypeDefinition
        {
            get
            {
                return this.t.IsGenericTypeDefinition;
            }
        }

        public bool IsGenericType
        {
            get
            {
                return this.t.IsGenericType;
            }
        }

        public bool IsGenericParameter
        {
            get
            {
                return this.t.IsGenericParameter;
            }
        }

        public bool IsExplicitLayout
        {
            get
            {
                return this.t.IsExplicitLayout;
            }
        }

        public bool IsEnum
        {
            get
            {
                return this.t.IsEnum;
            }
        }

        public bool IsCOMObject
        {
            get
            {
                return this.t.IsCOMObject;
            }
        }

        public System.Collections.Generic.IEnumerable<ConstructorInfo> DeclaredConstructors
        {
            get
            {
                return this.t.GetConstructors(BindingFlags.DeclaredOnly);
            }
        }

        public string Namespace
        {
            get
            {
                return this.t.Namespace;
            }
        }

        public TypeInfo()
        {
        }

        public TypeInfo(Type type)
        {
            this.t = type;
        }

        public Type AsType()
        {
            return this.t;
        }

        public Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
        {
            return this.t.FindInterfaces(filter, filterCriteria);
        }

        public MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
        {
            return this.t.FindMembers(memberType, bindingAttr, filter, filterCriteria);
        }

        public int GetArrayRank()
        {
            return this.t.GetArrayRank();
        }

        public ConstructorInfo GetConstructor(Type[] types)
        {
            return this.t.GetConstructor(types);
        }

        public ConstructorInfo[] GetConstructors()
        {
            return this.t.GetConstructors();
        }

        public ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
        {
            return this.t.GetConstructors(bindingAttr);
        }

        public EventInfo GetDeclaredEvent(string name)
        {
            EventInfo result;
            foreach (EventInfo ei in this.DeclaredEvents)
            {
                bool flag = string.Equals(ei.Name, name, StringComparison.Ordinal);
                if (flag)
                {
                    result = ei;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public FieldInfo GetDeclaredField(string name)
        {
            FieldInfo result;
            foreach (FieldInfo fi in this.DeclaredFields)
            {
                bool flag = string.Equals(fi.Name, name, StringComparison.Ordinal);
                if (flag)
                {
                    result = fi;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public MethodInfo GetDeclaredMethod(string name)
        {
            MethodInfo result;
            foreach (MethodInfo mi in this.DeclaredMethods)
            {
                bool flag = string.Equals(mi.Name, name, StringComparison.Ordinal);
                if (flag)
                {
                    result = mi;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public System.Collections.Generic.IEnumerable<MethodInfo> GetDeclaredMethods(string name)
        {
            System.Collections.Generic.List<MethodInfo> ls =
                new System.Collections.Generic.List<MethodInfo>();

            foreach (MethodInfo mi in this.DeclaredMethods)
            {
                bool flag = string.Equals(mi.Name, name, StringComparison.Ordinal);
                if (flag)
                {
                    ls.Add(mi);
                }
            }
            return ls;
        }

        public TypeInfo GetDeclaredNestedType(string name)
        {
            TypeInfo result;
            foreach (TypeInfo ti in this.DeclaredNestedTypes)
            {
                bool flag = string.Equals(ti.Name, name, StringComparison.Ordinal);
                if (flag)
                {
                    result = ti;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public PropertyInfo GetDeclaredProperty(string name)
        {
            PropertyInfo result;
            foreach (PropertyInfo pi in this.DeclaredProperties)
            {
                bool flag = string.Equals(pi.Name, name, StringComparison.Ordinal);
                if (flag)
                {
                    result = pi;
                    return result;
                }
            }
            result = null;
            return result;
        }

        public MemberInfo[] GetDefaultMembers()
        {
            return this.t.GetDefaultMembers();
        }

        public Type GetElementType()
        {
            return this.t.GetElementType();
        }

        // public string GetEnumName(object value) => t.GetEnumName(value);
        // public string[] GetEnumNames() => t.GetEnumNames();
        // public Type GetEnumUnderlyingType() => t.GetEnumUnderlyingType();
        // public Array GetEnumValues() => t.GetEnumValues();

        public EventInfo GetEvent(string name)
        {
            return this.t.GetEvent(name);
        }

        public EventInfo GetEvent(string name, BindingFlags bindingAttr)
        {
            return this.t.GetEvent(name, bindingAttr);
        }

        public EventInfo[] GetEvents()
        {
            return this.t.GetEvents();
        }

        public EventInfo[] GetEvents(BindingFlags bindingAttr)
        {
            return this.t.GetEvents(bindingAttr);
        }

        public FieldInfo GetField(string name)
        {
            return this.t.GetField(name);
        }

        public FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            return this.t.GetField(name, bindingAttr);
        }

        public FieldInfo[] GetFields()
        {
            return this.t.GetFields();
        }

        public FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            return this.t.GetFields(bindingAttr);
        }

        public Type[] GetGenericArguments()
        {
            return this.t.GetGenericArguments();
        }

        public Type[] GetGenericParameterConstraints()
        {
            return this.t.GetGenericParameterConstraints();
        }

        public Type GetGenericTypeDefinition()
        {
            return this.t.GetGenericTypeDefinition();
        }

        public Type GetInterface(string name)
        {
            return this.t.GetInterface(name);
        }

        public Type GetInterface(string name, bool ignoreCase)
        {
            return this.t.GetInterface(name, ignoreCase);
        }

        public Type[] GetInterfaces()
        {
            return this.t.GetInterfaces();
        }

        public MemberInfo[] GetMember(string name)
        {
            return this.t.GetMember(name);
        }

        public MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
        {
            return this.t.GetMember(name, type, bindingAttr);
        }

        public MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
        {
            return this.t.GetMember(name, bindingAttr);
        }

        public MemberInfo[] GetMembers()
        {
            return this.t.GetMembers();
        }

        public MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            return this.t.GetMembers(bindingAttr);
        }

        public MethodInfo GetMethod(string name)
        {
            return this.t.GetMethod(name);
        }

        public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
        {
            return this.t.GetMethod(name, bindingAttr);
        }

        public MethodInfo GetMethod(string name, Type[] types)
        {
            return this.t.GetMethod(name, types);
        }

        public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
        {
            return this.t.GetMethod(name, types, modifiers);
        }

        public MethodInfo[] GetMethods()
        {
            return this.t.GetMethods();
        }

        public MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            return this.t.GetMethods(bindingAttr);
        }

        public Type GetNestedType(string name)
        {
            return this.t.GetNestedType(name);
        }

        public Type GetNestedType(string name, BindingFlags bindingAttr)
        {
            return this.t.GetNestedType(name, bindingAttr);
        }

        public Type[] GetNestedTypes()
        {
            return this.t.GetNestedTypes();
        }

        public Type[] GetNestedTypes(BindingFlags bindingAttr)
        {
            return this.t.GetNestedTypes(bindingAttr);
        }

        public PropertyInfo[] GetProperties()
        {
            return this.t.GetProperties();
        }

        public PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            return this.t.GetProperties(bindingAttr);
        }

        public PropertyInfo GetProperty(string name, Type[] types)
        {
            return this.t.GetProperty(name, types);
        }

        public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            return this.t.GetProperty(name, returnType, types, modifiers);
        }

        public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
        {
            return this.t.GetProperty(name, returnType, types);
        }

        public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
        {
            return this.t.GetProperty(name, bindingAttr);
        }

        public PropertyInfo GetProperty(string name)
        {
            return this.t.GetProperty(name);
        }

        public PropertyInfo GetProperty(string name, Type returnType)
        {
            return this.t.GetProperty(name, returnType);
        }

        public bool IsAssignableFrom(Type c)
        {
            return this.t.IsAssignableFrom(c);
        }

        public bool IsAssignableFrom(TypeInfo typeInfo)
        {
            return this.t.IsAssignableFrom(typeInfo.t);
        }

        // public bool IsEnumDefined(object value) => t.IsEnumDefined(value);
        // public bool IsEquivalentTo(Type other) => t.IsEquivalentTo(other);

        public bool IsInstanceOfType(object o)
        {
            return this.t.IsInstanceOfType(o);
        }

        public bool IsSubclassOf(Type c)
        {
            return this.t.IsSubclassOf(c);
        }

        public Type MakeArrayType()
        {
            return this.t.MakeArrayType();
        }

        public Type MakeArrayType(int rank)
        {
            return this.t.MakeArrayType(rank);
        }

        public Type MakeByRefType()
        {
            return this.t.MakeByRefType();
        }

        public Type MakeGenericType(params Type[] typeArguments)
        {
            return this.t.MakeGenericType(typeArguments);
        }

        public Type MakePointerType()
        {
            return this.t.MakePointerType();
        }
    }
}

#endif
