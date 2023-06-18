import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Profile } from 'src/app/shared/interfaces/profile';
import { ProfileService } from 'src/app/shared/services/profiles.service';
import { Response } from 'src/app/shared/wrappers/response.wrapper';

@Component({
  selector: 'app-profiles-management',
  templateUrl: './profiles-management.component.html',
  styleUrls: ['./profiles-management.component.css']
})
export class ProfilesManagementComponent implements OnInit {

  public profiles: Profile[] = [];

  constructor(private profileService: ProfileService, private router: Router) {

  }

  ngOnInit(): void {
    this.fetchProfiles();
  }

  fetchProfiles() {
    this.profileService.getProfiles().subscribe((res: Response<any>) => {
      if (res.succeeded) {
        this.profiles = res.payload.values?.profiles;
      }
    });
  }

  editProfile(e: Profile) {
    this.router.navigate(['profiles/edit', e.profileId]);
  }

  deleteProfile(e: Profile) {
    if (e.profileId) {
      this.profileService.deleteProfile(e.profileId).subscribe((res: Response<any>) => {
        if (res.succeeded) {
          this.profiles = this.profiles.filter(p => p.profileId != e.profileId);
        }
      });
    }
  }

  createNewProfile() {
    this.router.navigate(['profiles/add']);
  }

  manageTask(e: Profile) {
    this.router.navigate(['tasks', e.profileId]);
  }
}
