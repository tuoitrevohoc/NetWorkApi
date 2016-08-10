/* tslint:disable:no-unused-variable */

import { By }           from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import {
  beforeEach, beforeEachProviders,
  describe, xdescribe,
  expect, it, xit,
  async, inject
} from '@angular/core/testing';

import { EntityComposerComponent } from './entity-composer.component';

describe('Component: EntityComposer', () => {
  it('should create an instance', () => {
    let component = new EntityComposerComponent();
    expect(component).toBeTruthy();
  });
});
