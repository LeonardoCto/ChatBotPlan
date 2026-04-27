
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Application;
using Microsoft.AspNetCore.Mvc;
using ChatBotPlan.Domain.Entities;

namespace ChatBotPlan.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(CreateUsersCases createUser, GetByIdUserCase getByUserId, UpdateUserCase update) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] UserRequestDTO user, CancellationToken ct)
    {
        var result = await createUser.ExecuteAsync(user, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await getByUserId.GetById(id, ct);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(UpdateUserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateUserDTO user, CancellationToken ct)
    {
        // Future- develop email update validation 
        //if (user.Email != null)
        //var result = await emailUpdate.ExecuteAsync();

        var result = await update.ExecuteAsync(id, user, ct);
        return Ok(result);
    }

    //[HttpDelete("{id}")]
    //[ProducesResponseType(typeof(UserResponseDTO))]

}