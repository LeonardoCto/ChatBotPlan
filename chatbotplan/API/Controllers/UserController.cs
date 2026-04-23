
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Application;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotPlan.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(CreateUsersCases createUser, GetByIdUserCase getUserById) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDTO user, CancellationToken ct)
    {
        var result = await createUser.ExecuteAsync(user, ct);
        // return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        return Ok(result);
    }

    // [HttpGet]
    // [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    // {
    //     var result = await getByUserId(id, ct);
    //     return Ok(result);
    // }

}