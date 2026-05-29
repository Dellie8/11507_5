using ReflectionClass.Homework.Utils.Validators.Abstraction;

namespace ReflectionClass.Homework.Utils.Validators.Implementation;

public class UniversalValidator : IValidator
{
        /// <summary>
        /// Универсальный метод, который валидирует ВООБЩЕ любой объект на основе его атрибутов.
        /// </summary>
        public bool Validate(object? obj, out List<string> errors)
        {
            errors = new List<string>();
            
            // TODO: Проверить на null
            if (obj == null)
            {
                errors.Add("Object cannot be null");
                return false;
            }
            
            // TODO: ШАГ 1. Получить тип объекта 
            Type objectType = obj.GetType();
            
            // TODO: ШАГ 2. Извлечь все свойства
            PropertyInfo[] properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            // TODO: ШАГ 3. Получать все значения свойств у ТЕКУЩЕГО экземпляра
            foreach (var property in properties)
            {
                object? value = property.GetValue(obj);

            // TODO: ШАГ 3.1 Проверять, обвешано ли свойство атрибутом MyRequired
                if (Attribute.IsDefined(property, typeof(MyRequiredAttribute)))
                {
                    if (value == null)
                    {
                        errors.Add($"Property '{property.Name}' is required but is null");
                    }
                    else if (value is string str && string.IsNullOrWhiteSpace(str))
                    {
                        errors.Add($"Property '{property.Name}' is required but is empty or whitespace");
                    }
                }
            // TODO: ШАГ 3.2 Проверять, есть ли атрибут MyRange
            var rangeAttr = property.GetCustomAttribute<MyRangeAttribute>();
            if (rangeAttr != null)
            {
                if (value is IComparable comparable)
                {
                    try
                    {
                        if (comparable.CompareTo(rangeAttr.Min) < 0 || comparable.CompareTo(rangeAttr.Max) > 0)
                        {
                            errors.Add($"Property '{property.Name}' value '{value}' is not in range [{rangeAttr.Min}, {rangeAttr.Max}]");
                        }
                    }
                    catch (Exception)
                    {
                        errors.Add($"Property '{property.Name}' cannot be compared with range values");
                    }
                }
                else if (value != null)
                {
                    errors.Add($"Property '{property.Name}' is not comparable (cannot apply range validation)");
                }
            }
            }
        
            return errors.Count == 0;
        }
}

[AttributeUsage(AttributeTargets.Property)]
public class MyRequiredAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Property)]
public class MyRangeAttribute : Attribute
{
    public object Min { get; }
    public object Max { get; }
    
    public MyRangeAttribute(object min, object max)
    {
        Min = min;
        Max = max;
    }
}