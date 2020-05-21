// An fS wrapper for the python time module

method time.asctime t does @ import time ; time.asctime( -t );
method time.gmtime secs does @ import time ; time.gmtime( -secs );
method time.time does @ import time ; time.time();
method time.localtime secs does @ import time ; @ time.localtime( -secs );
method time.pthread_getcpuclockid thread_id does @ import time ; time.pthread_getcpuclockid( -thread_id );
method time.clock_getres clk_id does @ import time ; time.clock_getres( -clk_id );
method time.clock_gettime clk_id does @ import time ; time.clock_gettime( -clk_id );
method time.clock_gettime_ns clk_id does @ import time ; time.clock_gettime_ns( -clk_id );
method time.clock_settime clk_id time does @ import time ; time.clock_settime( -clk_id , -time );
method time.ctime secs does @ import time ; time.ctime( -secs );
method time.get_clock_info name does @ import time ; time.get_clock_info( -name );
method time.mktime t does @ import time ; time.mktime( -t );
method time.monotonic does @ import time ; time.monotonic();
method time.monotonic_ns does @ import time ; time.monotonic_ns();
method time.perf_counter does @ import time ; time.perf_counter();
method time.perf_counter_ns does @ import time ; time.perf_counter_ns();
method time.process_time does @ import time ; time.process_time();
method time.process_time_ns does @ import time ; time.process_time_ns();
method time.sleep secs does @ import time ; time.sleep( -secs )
method time.strftime t does @ import time ; time.strftime( -t )
method time.strptime stringy does @ import time ; time.strptime( -stringy )
method time.thread_time does @ import time ; time.thread_time()
method time.thread_time_ns does @ import time ; time.thread_time_ns()
method time.time_ns does @ import time ; time.time_ns()
method time.tzset does @ import time ; time.tzset()
