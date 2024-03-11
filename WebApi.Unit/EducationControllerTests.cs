using Domain.Trainer;
using Infrastructure.ChatBot;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebApi.Features.Education;
using WebApi.Features.Education.GetAssignmentDetails;
using WebApi.Features.Education.GetStudentAssignmentList;
using WebApi.Features.Education.GetStudentSubjectList;
using WebApi.Features.Education.SetAssignmentSolution;
using WebApi.Features.Education.StudentSelfAssignment;

namespace WebApi.Unit;

public class EducationControllerTests
{
    [Fact]
    public async Task GetStudentAssignmentList_StudentExists_ReturnsList()
    {
        var request = new GetStudentAssignmentListRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(request).Returns(
            Task.FromResult<IList<Assignment>>(new List<Assignment>())
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result = await controller.GetStudentAssignmentList();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetStudentAssignmentList_StudentDoesntExist_ThrowsException()
    {
        var request = new GetStudentAssignmentListRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(request).Returns(
            Task.FromException<IList<Assignment>>(new InvalidOperationException())
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        await controller.Invoking(educationController => educationController.GetStudentAssignmentList())
            .Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task StudentSelfAssignment_HasExercisesAvailable_ReturnsAssignment()
    {
        var request = new StudentSelfAssignmentRequest(1, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(request).Returns(
            Task.FromResult(new Exercise())
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result = await controller.AddSelfAssignment(new StudentSelfAssignmentDto(request.SubjectId));

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetAssignmentDetails_ValidData_ReturnsAssignment()
    {
        var request = new GetAssignmentDetailsRequest(1, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(request).Returns(
            Task.FromResult(Assignment.Create(1, 1))
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result =
            await controller.GetAssignmentDetails(new GetAssignmentDetailsDto(request.ExerciseId));

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetAssignmentDetails_InvalidExerciseId_ThrowsExceptions()
    {
        var request = new GetAssignmentDetailsRequest(1, 0);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(request).Returns(
            Task.FromException<Assignment>(new InvalidOperationException())
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        await controller.Invoking(educationController =>
                educationController.GetAssignmentDetails(new GetAssignmentDetailsDto(request.ExerciseId)))
            .Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task SetAssignmentSolution_ValidData_ReturnsInfo()
    {
        var request = new SetAssignmentSolutionRequest(1, 1, "Решение");

        var mediator = Substitute.For<IMediator>();
        mediator.Send(request).Returns(
            Task.FromResult(new VerificationResult(true, null, null))
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result =
            await controller.SetAssignmentSolution(new SetAssignmentSolutionDto(request.ExerciseId, request.Solution));

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetStudentSubjectList_ValidData_ReturnsList()
    {
        var request = new GetStudentSubjectListRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(request).Returns(
            new List<Subject> { Subject.Create("Test") }
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result = await controller.GetStudentSubjectList();

        result.Should().BeOfType<OkObjectResult>();
    }
}