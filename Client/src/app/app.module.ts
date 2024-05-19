import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {AppRoutingModule} from "./app-routing.module";
import {VerifierComponent} from './components/verifier/verifier.component';
import {HomeComponent} from './components/home/home.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {CommonModule} from "@angular/common";
import {WelcomeComponent} from './components/welcome/welcome.component';
import {TrainerLayoutComponent} from './components/trainer-layout/trainer-layout.component';
import {TrainerInformationComponent} from './components/trainer-information/trainer-information.component';
import {TrainerEditorComponent} from './components/trainer-editor/trainer-editor.component';
import {TrainerComponent} from './components/trainer/trainer.component';
import {MonacoEditorModule} from "ngx-monaco-editor-v2";
import {TrainerMenuComponent} from './components/trainer-menu/trainer-menu.component';
import {CdkAccordionModule} from "@angular/cdk/accordion";
import {TrainerResultComponent} from './components/trainer-result/trainer-result.component';
import {TrainerOutputComponent} from './components/trainer-output/trainer-output.component';
import {AssignmentsComponent} from './components/assignments/assignments.component';
import {AssignmentsTableComponent} from './components/assignments-table/assignments-table.component';
import {CdkTableModule} from "@angular/cdk/table";
import {AssignmentsMenuComponent} from './components/assignments-menu/assignments-menu.component';
import {DummyRedirectComponent} from './components/dummy-redirect/dummy-redirect.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    VerifierComponent,
    WelcomeComponent,
    TrainerLayoutComponent,
    TrainerInformationComponent,
    TrainerEditorComponent,
    TrainerComponent,
    TrainerMenuComponent,
    TrainerResultComponent,
    TrainerOutputComponent,
    AssignmentsComponent,
    AssignmentsTableComponent,
    AssignmentsMenuComponent,
    DummyRedirectComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MonacoEditorModule.forRoot(),
    CdkAccordionModule,
    CdkTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
