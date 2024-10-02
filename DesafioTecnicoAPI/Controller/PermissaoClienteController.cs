using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PermissaoClienteController : ControllerBase
{
    private readonly AppDbContext _context;

    public PermissaoClienteController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PermissaoCliente>> GetPermissaoCliente(int id)
    {
        var cliente = await _context.PermissoesClientes.FindAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        return cliente;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePermissaoCliente(int id, PermissaoCliente cliente)
    {
        if (id != cliente.ClienteID)
        {
            return BadRequest();
        }

        _context.Entry(cliente).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PermissaoClienteExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool PermissaoClienteExists(int id)
    {
        return _context.PermissoesClientes.Any(e => e.ClienteID == id);
    }
}
