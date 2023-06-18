import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from 'src/app/shared/interfaces/task';
import { ProfileService } from 'src/app/shared/services/profiles.service';
import { TaskService } from 'src/app/shared/services/tasks.service';
import { Response } from 'src/app/shared/wrappers/response.wrapper';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit {

  public profileId: number = 0;
  public taskId: number = 0;
  public task: Task = new Task();

  constructor(private taskService: TaskService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    let pid = this.route.snapshot.paramMap.get('profileId');
    let tid = this.route.snapshot.paramMap.get('taskId');

    if (pid && tid) {
      this.profileId = +pid;
      this.taskId = +tid;

      this.fetchTask();
    }
    else {
      this.router.navigate(['profiles']);
    }
  }

  submitForm(e: Task) {
    this.taskService.updateTask(e).subscribe((res: Response<any>) => {
      if (res.succeeded) {
        this.router.navigate(['tasks', this.profileId]);
      }
      else {
        alert(res.message);
      }
    });
  }

  fetchTask() {
    this.taskService.getTasksById(this.taskId).subscribe((res: Response<any>) => {
      if (res.succeeded) {
        this.task = res.payload?.value?.task;
      }
      else {
        alert(res.message);
      }
    });
  }
}
