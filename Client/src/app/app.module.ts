import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {EducationComponent} from './components/education/education.component';
import {AppRoutingModule} from "./app-routing.module";
import {ExerciseDetailsComponent} from "./components/exercise-details/exercise-details.component";
import {ExerciseSolutionComponent} from "./components/exercise-solution/exercise-solution.component";
import {VerifierComponent} from './components/verifier/verifier.component';
import {HomeComponent} from './components/home/home.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ExerciseResultComponent} from "./components/exercise-result/exercise-result.component";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatTableModule} from "@angular/material/table";
import {EducationExercisesComponent} from "./components/education-exercises/education-exercises.component";
import {EducationControlComponent} from "./components/education-control/education-control.component";
import {ExerciseComponent} from "./components/exercise/exercise.component";
import {EducationRatingComponent} from './components/education-rating/education-rating.component';
import {EducationHistoryComponent} from './components/education-history/education-history.component';
import {MatPaginatorModule} from "@angular/material/paginator";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    EducationComponent,
    EducationExercisesComponent,
    EducationControlComponent,
    EducationRatingComponent,
    EducationHistoryComponent,
    ExerciseComponent,
    ExerciseDetailsComponent,
    ExerciseSolutionComponent,
    ExerciseResultComponent,
    VerifierComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
