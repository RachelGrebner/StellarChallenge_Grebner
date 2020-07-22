Assumptions:
* get all, get by name, like, and create are all that's needed
* using local DateTime is sufficient for calculating expiry, may be messy if different time zones were invloved

Concerns
* there are no tests, and there should be lots.
* spent majority of time trying to write the controller cleanly that not I haven;t had adequate time to test all enpoints using external tools (in my case PostMan)
* DataStore singleton is not the best way to tackle data storage obviuously. Will get reset every time app is restarted