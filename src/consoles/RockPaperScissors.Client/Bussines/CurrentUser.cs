using RockPaperScissors.Client.Bussines.Abstractions;

namespace RockPaperScissors.Client.Bussines;
public class CurrentUser : ICurrentUser {

    private string _name;
    private int _id;

    private static readonly object _locker = new object();
    static ICurrentUser instance;
    private CurrentUser (string name, int id) { 
        this._name = name;
        this._id = id;
    }

    public string GetName () {
        return _name;
    }

    public int GetId () {
        return _id;
    }

    public static CurrentUser GetInstance(string name = null, int id = 0) {
        lock (_locker) {
            if (instance is null) {
                if (!string.IsNullOrWhiteSpace(name) && id > 0) {
                    instance = new CurrentUser(name, id);
                    return (CurrentUser)instance;
                } else { return (CurrentUser)instance; }
            } else {
                return (CurrentUser)instance;
            }
        }
    }
}
