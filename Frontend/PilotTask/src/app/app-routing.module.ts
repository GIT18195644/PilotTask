import { ModuleWithProviders, NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { TasksManagementComponent } from './component/tasks-management/tasks-management.component';
import { ProfilesManagementComponent } from './component/profiles-management/profiles-management.component';
import { CreateProfileComponent } from './component/profiles-management/create-profile/create-profile.component';
import { EditProfileComponent } from './component/profiles-management/edit-profile/edit-profile.component';
import { CreateTaskComponent } from './component/tasks-management/create-task/create-task.component';
import { EditTaskComponent } from './component/tasks-management/edit-task/edit-task.component';

const routes: Routes = [
  { path: 'tasks/:profileId', component: TasksManagementComponent },
  { path: 'tasks/:profileId/add', component: CreateTaskComponent },
  { path: 'tasks/:profileId/edit/:taskId', component: EditTaskComponent },
  { path: 'profiles', component: ProfilesManagementComponent },
  { path: 'profiles/add', component: CreateProfileComponent },
  { path: 'profiles/edit/:profileId', component: EditProfileComponent },
  { path: '**', redirectTo: '/profiles', pathMatch: 'full' }
];

export const AppRoutingModule: ModuleWithProviders<any> = RouterModule.forRoot(routes, {
  preloadingStrategy: PreloadAllModules
});
