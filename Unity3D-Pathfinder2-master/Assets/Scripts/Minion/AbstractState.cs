using UnityEngine;

public abstract class AbstractState : MonoBehaviour
{
   protected Context _context=new Context();

        public void SetContext(Context context)
        {
            _context = context;
        }

       // public abstract void Handle1();

       // public abstract void Handle2();
}
