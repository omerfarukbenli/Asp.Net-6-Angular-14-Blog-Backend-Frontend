import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/models/post.model';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-admin-posts',
  templateUrl: './admin-posts.component.html',
  styleUrls: ['./admin-posts.component.css']
})
export class AdminPostsComponent implements OnInit {

  constructor(private postService:PostService) { }


post:Post[] = [];

  ngOnInit(): void {
    this.postService.getAllPosts()
    .subscribe(
      response => {
this.post = response;
      }
    );

  }

}
