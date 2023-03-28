using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TesteVericode.Core.Exceptions;
using TesteVericode.Core.Handlers.Command;
using TesteVericode.Core.Handlers.Query;
using TesteVericode.Domain.DTO;
using TesteVericode.Domain.RabbitMQ;

namespace TesteVericode.API.Controllers
{
    /// <summary>
    /// Tarefa Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ILogger<TarefaController> _logger;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor Tarefa Controller
        /// </summary>
        public TarefaController(
            ILogger<TarefaController> logger,
            IRabbitMQService rabbitMQService,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _rabbitMQService = rabbitMQService;
        }

        /// <summary>
        /// Recupera todas as Tarefas
        /// </summary>
        /// <response code="200">Tarefas recuperadas</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TarefaDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllTarefasQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Cria uma nova Tarefa
        /// </summary>
        /// <response code="201">Tarefa Adicionada</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Tarefa([FromBody] CreateTarefaDTO model)
        {
            try
            {
                var command = new CreateTarefaCommand(model);
                var response = await _mediator.Send(command);

                return StatusCode((int)HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        /// <summary>
        /// Recupera uma Tarefa pelo id
        /// </summary>
        /// <response code="200">Tarefa recuperada</response>
        /// <response code="404">Tarefa não encontrada</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(TarefaDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetTarefaByIdQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        /// <summary>
        /// Deleta uma Tarefa
        /// </summary>
        /// <response code="200">Tarefa deletada</response>
        /// <response code="400">Bad Request</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteTarefaCommand(id);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        /// <summary>
        /// Atualiza uma Tarefa
        /// </summary>
        /// <response code="200">Tarefa atualizada</response>
        /// <response code="400">Bad Request</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] UpdateTarefaDTO model, int id)
        {
            try
            {
                var command = new UpdateTarefaCommand(model, id);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

    }
}