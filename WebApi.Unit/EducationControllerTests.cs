using Domain.Trainer;
using Infrastructure.ChatBot;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebApi.Features.Education;
using WebApi.Features.Education.GetAssignment;
using WebApi.Features.Education.GetAssignments;
using WebApi.Features.Education.GetSubjects;
using WebApi.Features.Education.SelfAssignment;
using WebApi.Features.Education.SetAssignmentSolution;

namespace WebApi.Unit;

public class EducationControllerTests
{
    [Fact]
    public async Task Test1()
    {
        var getAssignmentsRequest = new GetAssignmentsRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentsRequest).Returns(
            Task.FromResult(null as IList<Assignment>)
        );
        var controller = new EducationController(mediator);
        var result = await controller.GetMyAssignments();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Test2()
    {
        var getAssignmentsRequest = new GetAssignmentsRequest(0);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentsRequest).Returns(
            Task.FromException<IList<Assignment>>(new InvalidOperationException())
        );
        var controller = new EducationController(mediator);
        await controller.Invoking(educationController => educationController.GetMyAssignments()).Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Test3()
    {
        var selfAssignmentRequest = new SelfAssignmentRequest(1, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(selfAssignmentRequest).Returns(
            Task.FromResult(null as Exercise)
        );
        var controller = new EducationController(mediator);
        var result = await controller.AddSelfAssignment(selfAssignmentRequest);

        result.Should().BeOfType<StatusCodeResult>();
        // Выдавал ошибку, исправили, отметить в презе
        (result as StatusCodeResult).StatusCode.Should().Be(501);
    }

    [Fact]
    public async Task Test4()
    {
        var selfAssignmentRequest = new SelfAssignmentRequest(1, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(selfAssignmentRequest).Returns(
            Task.FromResult(new Exercise())
        );
        var controller = new EducationController(mediator);
        var result = await controller.AddSelfAssignment(selfAssignmentRequest);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Test5()
    {
        var selfAssignmentRequest = new SelfAssignmentRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(selfAssignmentRequest).Returns(
            Task.FromException<Exercise>(new InvalidOperationException())
        );
        var controller = new EducationController(mediator);
        await controller.Invoking(educationController => educationController.AddSelfAssignment(selfAssignmentRequest))
            .Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Test6()
    {
        var getAssignmentRequest = new GetAssignmentRequest(1, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentRequest).Returns(
            Task.FromResult(new Assignment(1, 1))
        );
        var controller = new EducationController(mediator);
        var result = await controller.GetAssignment(1);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Test7()
    {
        var getAssignmentRequest = new GetAssignmentRequest(1, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentRequest).Returns(
            Task.FromResult(null as Assignment)
        );
        var controller = new EducationController(mediator);
        var result = await controller.GetAssignment(1);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Test8()
    {
        var getAssignmentRequest = new GetAssignmentRequest(1, 0);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentRequest).Returns(
            Task.FromException<Assignment>(new InvalidOperationException())
        );
        var controller = new EducationController(mediator);
        await controller.Invoking(educationController => educationController.GetAssignment(0)).Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Test9()
    {
        var getAssignmentRequest = new GetAssignmentRequest(0, 1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getAssignmentRequest).Returns(
            Task.FromException<Assignment>(new InvalidOperationException())
        );
        var controller = new EducationController(mediator);
        await controller.Invoking(educationController => educationController.GetAssignment(1)).Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Test10()
    {
        var setAssignmentSolutionRequest = new SetAssignmentSolutionRequest(1, 1, "Решение");

        var mediator = Substitute.For<IMediator>();
        mediator.Send(setAssignmentSolutionRequest).Returns(
            Task.FromResult(new VerificationResult(true, null, null))
        );
        var controller = new EducationController(mediator);
        var result = await controller.SetAssignmentSolution(setAssignmentSolutionRequest);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Test11()
    {
        var setAssignmentSolutionRequest = new SetAssignmentSolutionRequest(1, 0, "Решение");

        var mediator = Substitute.For<IMediator>();
        mediator.Send(setAssignmentSolutionRequest).Returns(
            Task.FromResult(null as VerificationResult)
        );
        var controller = new EducationController(mediator);
        var result = await controller.SetAssignmentSolution(setAssignmentSolutionRequest);

        result.Should().BeOfType<StatusCodeResult>();
        // Выдавал ошибку, исправили, отметить в презе
        (result as StatusCodeResult).StatusCode.Should().Be(501);
    }

    [Fact]
    public async Task Test12()
    {
        var getSubjectsRequest = new GetSubjectsRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getSubjectsRequest).Returns(
            Task.FromResult(null as IList<Subject>)
        );
        var controller = new EducationController(mediator);
        var result = await controller.GetMySubjects();

        result.Should().BeOfType<StatusCodeResult>();
        // Выдавал ошибку, исправили, отметить в презе
        (result as StatusCodeResult).StatusCode.Should().Be(501);
    }

    [Fact]
    public async Task Test13()
    {
        var getSubjectsRequest = new GetSubjectsRequest(1);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getSubjectsRequest).Returns(
            new List<Subject> { new() }
        );
        var controller = new EducationController(mediator);
        var result = await controller.GetMySubjects();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Test14()
    {
        var getSubjectsRequest = new GetSubjectsRequest(0);

        var mediator = Substitute.For<IMediator>();
        mediator.Send(getSubjectsRequest).Returns(
            Task.FromException<IList<Subject>>(new InvalidOperationException())
        );
        var controller = new EducationController(mediator);
        await controller.Invoking(educationController => educationController.GetMySubjects()).Should()
            .ThrowAsync<InvalidOperationException>();
    }
}