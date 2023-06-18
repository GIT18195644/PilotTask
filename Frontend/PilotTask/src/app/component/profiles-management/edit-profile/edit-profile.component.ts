import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Profile } from 'src/app/shared/interfaces/profile';
import { ProfileService } from 'src/app/shared/services/profiles.service';
import { Response } from 'src/app/shared/wrappers/response.wrapper';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  public profileId: number = 0;
  public profile: Profile = new Profile();

  constructor(private profileService: ProfileService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('profileId');
    if (id) {
      this.profileId = +id;
      this.fetchProfile();
    }
    else {
      this.router.navigate(['profiles']);
    }
  }

  fetchProfile() {
    this.profileService.getProfileById(this.profileId).subscribe((res: Response<any>) => {
      if (res.succeeded) {
        this.profile = res.payload?.value?.profile;
      }
      else {
        alert(res.message);
      }
    });
  }

  submitForm(e: Profile) {
    this.profileService.updateProfile(e).subscribe((res: Response<any>) => {
      if (res.succeeded) {
        this.router.navigate(['profiles']);
      }
      else {
        alert(res.message);
      }
    });
  }
}
