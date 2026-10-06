using Invex.Repository.Context;
public abstract class BaseRepository
{
    protected readonly InvexContext _context;

    protected BaseRepository(InvexContext context)
    {
        _context = context;
    } 
}