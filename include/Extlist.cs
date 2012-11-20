using System;
using System.Collections.Generic;

//=======================================
//Реализует класс списка ExtList c допол-
//нительными методами для создания систе-
//мы событий при изменении его элементов.
//
//BeforeChanged - вызывется перед
//AfterChanged  - и после изменения
//
//using:
// shapelist.BeforeChanged += 
// new ChangedEventHandler(
// 	shapelist_BeforeChanged);
//
//=======================================

namespace Usermods.ExtList
{
    //handler delegate
    public delegate void ChangedEventHandler(object sender);
	
	//ExtList<T>
    public class ExtList<T> : List<T>
    {
        //разрешает или запрещает вызов событий - используется при множественном изменении (при загрузке),
		//когда генерируется слишком много ненужных событий.
		public bool Silent = false;
        public event ChangedEventHandler BeforeChanged;
        public event ChangedEventHandler AfterChanged;

        protected virtual void OnBeforeChanged()
        {
            if ((BeforeChanged != null) && (!Silent))
                BeforeChanged(this);
        }

        protected virtual void OnAfterChanged()
        {
            if ((BeforeChanged != null) && (!Silent))
                AfterChanged(this);
        }

        public new void Add(T value)
        {
            OnBeforeChanged();
            base.Add(value);
            OnAfterChanged();
        }

        public new void Clear()
        {
            OnBeforeChanged();
            base.Clear();
            OnAfterChanged();
        }

        public new T this[int index]
        {
            set
            {
                OnBeforeChanged();
                base[index] = value;
                OnAfterChanged();
            }

            get { return (base[index]); }
        }
    }
}
