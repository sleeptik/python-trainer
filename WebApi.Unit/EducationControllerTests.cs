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
        var getAssignmentsRequest = new GetStudentAssignmentListRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentsRequest).Returns(
            Task.FromResult(null as IList<Assignment>)
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result = await controller.GetMyAssignments();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetStudentAssignmentList_StudentDoesntExist_ThrowsException()
    {
        var getAssignmentsRequest = new GetStudentAssignmentListRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentsRequest).Returns(
            Task.FromException<IList<Assignment>>(new InvalidOperationException())
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        await controller.Invoking(educationController => educationController.GetMyAssignments())
            .Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task StudentSelfAssignment_HasExercisesAvailable_ReturnsAssignment()
    {
        var selfAssignmentRequest = new StudentSelfAssignmentRequest(1, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(selfAssignmentRequest).Returns(
            Task.FromResult(new Exercise())
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result = await controller.AddSelfAssignment(selfAssignmentRequest.SubjectId);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetAssignmentDetails_ValidData_ReturnsAssignment()
    {
        var getAssignmentRequest = new GetAssignmentDetailsRequest(1, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentRequest).Returns(
            Task.FromResult(new Assignment(1, 1))
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result = await controller.GetAssignment(1);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetAssignmentDetails_InvalidExerciseId_ThrowsExceptions()
    {
        var getAssignmentRequest = new GetAssignmentDetailsRequest(1, 0);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentRequest).Returns(
            Task.FromException<Assignment>(new InvalidOperationException())
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        await controller.Invoking(educationController => educationController.GetAssignment(0)).Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task SetAssignmentSolution_ValidData_ReturnsInfo()
    {
        var setAssignmentSolutionRequest = new SetAssignmentSolutionRequest(1, 1, "Решение");

        var mediator = Substitute.For<IMediator>();
        mediator.Send(setAssignmentSolutionRequest).Returns(
            Task.FromResult(new VerificationResult(true, null, null))
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result = await controller.SetAssignmentSolution(setAssignmentSolutionRequest);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetStudentSubjectList_ValidData_ReturnsList()
    {
        var getSubjectsRequest = new GetStudentSubjectListRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getSubjectsRequest).Returns(
            new List<Subject> { new() }
        );

        var controller = new EducationController(mediator)
        {
            ControllerContext = MockControllerContextFactory.CreateControllerContextWithUserIdClaim()
        };

        var result = await controller.GetMySubjects();

        result.Should().BeOfType<OkObjectResult>();
    }
}