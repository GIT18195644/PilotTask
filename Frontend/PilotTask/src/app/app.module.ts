import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProfileService } from './shared/services/profiles.service';
import { TaskService } from './shared/services/tasks.service';
import { TasksManagementComponent } from './component/tasks-management/tasks-management.component';
import { ProfilesManagementComponent } from './component/profiles-management/profiles-management.component';
import { HttpClientModule } from '@angular/common/http';
import { EditProfileComponent } from './component/profiles-management/edit-profile/edit-profile.component';
import { CreateProfileComponent } from './component/profiles-management/create-profile/create-profile.component';
import { FormsModule } from '@angular/forms';
import { CreateTaskComponent } from './component/tasks-management/create-task/create-task.component';
import { EditTaskComponent } from './component/tasks-management/edit-task/edit-task.component';

@NgModule({
  declarations: [
    AppComponent,
    TasksManagementComponent,
    ProfilesManagementComponent,
    EditProfileComponent,
    CreateProfileComponent,
    CreateTaskComponent,
    EditTaskComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    ProfileService,
    TaskService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
