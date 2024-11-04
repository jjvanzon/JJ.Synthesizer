            if (op.IsConst())
            {
                _sb.Append(op.AsConst());
                _sb.Append(FormatConst(op));
                return;
            }
        // Const formatting
        
        private string FormatConst(Operator op)
        {
            if (NameIsOperatorTypeName(op.Name, op.OperatorTypeName))
            {
                return $"{op.AsConst()}";
            }
            else
            {
                return $"{op.Name} {op.AsConst()}";
            }
        }


        /// <summary>
        /// Checks if the option for short notation is on in the first place. <br/>
        /// Also checks for specific operators that can use short notation: (<c>+ - * /</c>). <br/>
        /// Also checks if the operator has a specific name assigned, so that stops short notation from happening too,
        /// so the name will show e.g.<br/>
        /// <c>Tremolo Multiply( ... , ...)</c>
        /// </summary>
        //private bool CanOmitName(Operator op)
        //{
        //    if (!_canOmitNameForBasicMath)
        //    {
        //        return false;
        //    }

        //    bool nameIsDefault = !NameIsOperatorTypeName(op.Name, op.OperatorTypeName);
        //    if (!nameIsDefault)
        //    {
        //        return false;
        //    }

        //    if (op.IsAdd()) return true;
        //    if (op.IsSubtract()) return true;

        //    if (op.IsMultiply() || op.IsDivide())
        //    {
        //        return op.Origin() == null;
        //    }

        //    return false;
        //}
