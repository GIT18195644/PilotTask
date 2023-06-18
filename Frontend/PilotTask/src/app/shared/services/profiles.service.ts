import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from "src/environments/environment";
import { Response } from "../wrappers/response.wrapper";
import { Profile } from "../interfaces/profile";

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    constructor(private http: HttpClient) { }

    createProfile(profile: Profile) {
        const url = `${environment.baseUrl}/api/v1.0/Profiles`;
        const obj = {
            FirstName: profile.firstName,
            LastName: profile.lastName,
            DateOfBirth: profile.dateOfBirth,
            PhoneNumber: profile.phoneNumber,
            EmailId: profile.emailId
        };
        return this.http.post<Response<any>>(url, obj);
    }

    updateProfile(profile: Profile) {
        const url = `${environment.baseUrl}/api/v1.0/Profiles/${profile.profileId}`;
        const obj = {
            FirstName: profile.firstName,
            LastName: profile.lastName,
            DateOfBirth: profile.dateOfBirth,
            PhoneNumber: profile.phoneNumber,
            EmailId: profile.emailId
        };
        return this.http.put<Response<any>>(url, obj);
    }

    deleteProfile(profileId: number) {
        const url = `${environment.baseUrl}/api/v1.0/Profiles/${profileId}`;
        return this.http.delete<Response<any>>(url);
    }

    getProfiles() {
        const url = `${environment.baseUrl}/api/v1.0/Profiles`;
        return this.http.get<Response<any>>(url);
    }

    getProfileById(profileId: number) {
        const url = `${environment.baseUrl}/api/v1.0/Profiles/${profileId}`;
        return this.http.get<Response<any>>(url);
    }

    getTasksByProfileId(profileId: number) {
        const url = `${environment.baseUrl}/api/v1.0/Profiles/${profileId}/tasks`;
        return this.http.get<Response<any>>(url);
    }
}