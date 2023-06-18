import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from 'src/app/shared/interfaces/task';
import { TaskService } from 'src/app/shared/services/tasks.service';
import { Response } from 'src/app/shared/wrappers/response.wrapper';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent {
  public profileId: number = 0;
  public task: Task = new Task();

  constructor(private taskService: TaskService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    let pid = this.route.snapshot.paramMap.get('profileId');

    if (pid) {
      this.profileId = +pid;
    }
    else {
      this.router.navigate(['profiles']);
    }
  }

  submitForm(e: Task) {
    e.profileId = this.profileId;
    this.taskService.createTask(e).subscribe((res: Response<any>) => {
      if (res.succeeded) {
        this.router.navigate(['tasks', this.profileId]);
      }
      else {
        alert(res.message);
      }
    });
  }
}
