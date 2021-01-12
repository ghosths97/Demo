using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Services.Roles;
using Demo.Shared.Security;
using Demo.Shared.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MachineTalk.VMS.Authentication.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Policy = Permissions.Role)]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RoleRequest roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    return BadRequest();
                }
                var roleModel = new RolePermissionModel()
                {
                    Name = roleDto.Name,
                    Permissions = roleDto.Permissions   ,
                    RoleId = roleDto.RoleId
                    
                }; 
                var role = await _roleService.AddRole(roleModel);
                if (role != null)
                {
                    var roleResponseDto = new RoleResponse { Name = role.Name, Permissions = role.Permissions, RoleId = role.RoleId };
                    return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, roleResponseDto);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [Authorize(Policy = Permissions.Role)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var role = await _roleService.GetRole(id);
                var roleDto = new RoleResponse { Name = role.Name, Permissions = role.Permissions, RoleId = role.RoleId };
                return Ok(roleDto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetRolesByIds")]
        public async Task<IActionResult> GetRoleNamesByIds([FromQuery]string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var role = await _roleService.GetRoleNamesByIds(id);
                return Ok(role);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Policy = Permissions.Role)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleService.GetRole(id);
            var roleDto = new RoleResponse { Name = role.Name, Permissions = role.Permissions, RoleId = role.RoleId };
            return Ok(roleDto);
        }

        [Authorize(Policy = Permissions.Role)]
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            try
            {
                var roles = await _roleService.GetAllRole();
                var roleDto = new List<RoleResponse>();
                foreach (var role in roles)
                {
                    roleDto.Add(new RoleResponse { Name = role.Name, Permissions = role.Permissions, RoleId = role.RoleId });
                }
                return Ok(roleDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

      
        [HttpGet("RoleExists/{name}")]
        public async Task<IActionResult> RoleExists(string name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest();
                }
                var exist = await _roleService.IsRoleExists(name);
                if (exist)
                {
                    return Ok(new { message = "exists" });
                }
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [Authorize(Policy = Permissions.Role)]
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleRequest roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    return BadRequest();
                }
                var role = new RolePermissionModel()
                {
                    Name = roleDto.Name,
                    Permissions = roleDto.Permissions   ,
                    RoleId = roleDto.RoleId
                    
                }; 
                var result = await _roleService.UpdateRole(role);
                if (result)
                {
                    return Ok(roleDto);
                }

                return UnprocessableEntity(new { message = $"Unable to update role", key = "restrict" });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }       

        [HttpGet("permissions")]
        public IActionResult GetPermissions()
        {
            try
            {
                var permissionList = _roleService.GetAllPermissions();
                return Ok(permissionList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
