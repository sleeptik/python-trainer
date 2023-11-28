import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {EducationComponent} from './components/education/education.component';
import {AppRoutingModule} from "./app-routing.module";
import {ExerciseDetailsComponent} from "./components/exercise-details/exercise-details.component";
import {ExerciseSolutionComponent} from "./components/exercise-solution/exercise-solution.component";
import {VerifierComponent} from './components/verifier/verifier.component';

@NgModule({
  declarations: [
    AppComponent,
    EducationComponent,
    ExerciseDetailsComponent,
    ExerciseSolutionComponent,
    VerifierComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
