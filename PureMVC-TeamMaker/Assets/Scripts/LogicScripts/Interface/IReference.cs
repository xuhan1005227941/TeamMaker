//引用池类,所有需要进池的类均继承与此
public interface IReference
{
    void OnRequire();
    void OnRelease();
}

