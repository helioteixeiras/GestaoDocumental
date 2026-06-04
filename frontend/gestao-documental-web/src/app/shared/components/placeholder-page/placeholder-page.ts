import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-placeholder-page',
  imports: [MatCardModule],
  templateUrl: './placeholder-page.html',
  styleUrl: './placeholder-page.css',
})
export class PlaceholderPage {
  private readonly route = inject(ActivatedRoute);

  readonly title = this.route.snapshot.data['title'] as string;
  readonly description =
    (this.route.snapshot.data['description'] as string) ??
    'Módulo em preparação. A integração com a API será implementada numa fase seguinte.';
}
