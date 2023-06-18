import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from 'src/app/shared/interfaces/task';
import { ProfileService } from 'src/app/shared/services/profiles.service';
import { TaskService } from 'src/app/shared/services/tasks.service';
import { Response } from 'src/app/shared/wrappers/response.wrapper';

@Component({
  selector: 'app-tasks-management',
  templateUrl: './tasks-management.component.html',
  styleUrls: ['./tasks-management.component.css']
})
export class TasksManagementComponent implements OnInit {

  public tasks: Task[] = [];
  public profileId: number = 0;

  constructor(private profileService: ProfileService,
    private taskService: TaskService,
    private router: Router,
    private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('profileId');
    if (id) {
      this.profileId = +id;
      this.fetchTasks();
    }
    else {
      this.router.navigate(['profiles']);
    }
  }

  createNewTask() {
    this.router.navigate(['tasks', this.profileId, 'add']);
  }

  fetchTasks() {
    this.profileService.getTasksByProfileId(this.profileId).subscribe((res: Response<any>) => {
      if (res.succeeded) {
        this.tasks = res.payload?.values?.tasks;
      }
      else {
        alert(res.message);
      }
    });
  }

  editTask(e: Task) {
    this.router.navigate(['tasks', this.profileId, 'edit', e.id]);
  }

  deleteTask(e: Task) {
    if (e.id) {
      this.taskService.deleteTask(e.id).subscribe((res: Response<any>) => {
        if (res.succeeded) {
          this.fetchTasks();
        }
        else {
          alert(res.message);
        }
      });
    }
  }
}
