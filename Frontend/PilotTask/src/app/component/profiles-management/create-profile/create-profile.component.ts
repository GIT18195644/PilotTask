import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Profile } from 'src/app/shared/interfaces/profile';
import { ProfileService } from 'src/app/shared/services/profiles.service';
import { Response } from 'src/app/shared/wrappers/response.wrapper';

@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.component.html',
  styleUrls: ['./create-profile.component.css']
})
export class CreateProfileComponent {

  constructor(private profileService: ProfileService, private router: Router) {

  }

  public profile: Profile = new Profile();

  submitForm(e: Profile) {
    this.profileService.createProfile(e).subscribe((res: Response<any>) => {
      if (res.succeeded) {
        this.router.navigate(['profiles']);
      }
      else {
        alert(res.message);
      }
    });
  }
}
