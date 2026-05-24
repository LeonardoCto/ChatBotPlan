
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Application;
using Microsoft.AspNetCore.Mvc;
using ChatBotPlan.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Authorization;

namespace ChatBotPlan.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(CreateUsersUseCase createUser, GetByIdUserUseCase getByUserId, UpdateUserUseCase update, UpdateEmailUseCase updateEmail, DeleteUserUseCase delete) : ControllerBase
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

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await getByUserId.GetById(id, ct);
        return Ok(result);
    }

    [Authorize]
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

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        await delete.DeleteUser(id, ct);
        return NoContent();
    }

    [Authorize]
    [HttpPatch]
    [HttpPatch("update-email/{newEmail}")]
    [ProducesResponseType(typeof(UpdateUserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmail([FromRoute] string newEmail, [FromBody] UpdateUserDTO user, CancellationToken ct)
    {
        await updateEmail.ExecuteAsync(newEmail, user, ct);
        return NoContent();
    }

    [Authorize]
    [HttpPatch]
    [HttpPatch("confirm-email-update/{code}")]
    [ProducesResponseType(typeof(UpdateUserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmailUpdate([FromRoute] string code, [FromBody] UpdateUserDTO user, CancellationToken ct)
    {
        await updateEmail.ConfirmEmailChangeAsync(code, user, ct);
        return NoContent();
    }

}