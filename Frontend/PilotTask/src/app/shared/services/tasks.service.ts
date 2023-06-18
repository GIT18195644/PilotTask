import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Response } from "../wrappers/response.wrapper";
import { Task } from "../interfaces/task";

@Injectable({
    providedIn: 'root'
})
export class TaskService {
    constructor(private http: HttpClient) { }

    createTask(task: Task) {
        const url = `${environment.baseUrl}/api/v1.0/Tasks`;
        return this.http.post<Response<any>>(url, task);
    }

    updateTask(task: Task) {
        const url = `${environment.baseUrl}/api/v1.0/Tasks/${task.id}`;
        return this.http.put<Response<any>>(url, task);
    }

    deleteTask(taskId: number) {
        const url = `${environment.baseUrl}/api/v1.0/Tasks/${taskId}`;
        return this.http.delete<Response<any>>(url);
    }

    getTasksById(taskId: number) {
        const url = `${environment.baseUrl}/api/v1.0/Tasks/${taskId}`;
        return this.http.get<Response<any>>(url);
    }
}